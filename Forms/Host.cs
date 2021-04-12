using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using A6_TCP.Security;
using System.Data;
using System.Linq;
using System.Net;
using System.IO;
using Standards;
using System;

namespace A6_TCP.Forms
{
    public partial class Host : Form
    {
        private TcpListener listener;
        private ClientManager mngr;
        public static List<ClientManager> Client_List = new List<ClientManager>();

        public Host()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //Grabs and stores the current computers' IP
            IPAddress servername = (IPAddress)CbIP.SelectedValue;

            //Makes the listener
            listener = new TcpListener(servername, DataProperties.Port);
            listener.Start();

            WriteLog($"Server Started on {servername}:{DataProperties.Port}");

            NewClient();
        }

        private void NewClient()
        {
            //Builds a new Manager (Memory leaks?)
            mngr = new ClientManager(listener);
            mngr.NewClientConnected += Mngr_NewClientConnected;
            mngr.ClientDisconnected += Mngr_ClientDisconnected;
            mngr.ReceivedMessage += Mngr_ReceivedMessage;
            mngr.ReceivedFile += Mngr_ReceivedFile;
            mngr.SetUserName += Mngr_SetUserName;
            mngr.RecievedCommand += Mngr_RecievedCommand;
        }

        private void Mngr_SetUserName(ClientManager UserName)
        {
            LogMessage($"User {UserName.Client_ID} has been renamed to {UserName.Client_Name}");
        }

        private void Mngr_NewClientConnected(ClientManager client)
        {
            //Adds the client to the List
            Client_List.Add(client);

            //Gotta figure this out~
            if(client.Client_Name != null)
                LogMessage($">>>>Client#{client.Client_Name} has connected");
            else
                LogMessage($">>>>Client#{client.Client_ID} has connected");
            

            NewClient();
        }

        private void LogMessage(string Message)
        {
            WriteLog(Message);
            RelayMessage(Message);
        }

        private void Mngr_ClientDisconnected(ClientManager client)
        {
            if (Client_List.Remove(client))
            {
                LogMessage($"{client.Client_ID} has disconnected!");
                //Decrements the users in list
                ClientManager.ClientCounter--;
            }
            else
                WriteLog($"{client.Client_Name} doesn't exist!", EventLogEntryType.Warning);   
        }

        private void Mngr_ReceivedMessage(ClientManager client, string message)
        {
            string msg = $">>>>{client.Client_Name}:{message}";
            lstUMessage.Items.Add(msg);
            RelayMessage(msg);
        }

        private void Mngr_RecievedCommand(ClientManager client, CommandRequest request)
        {
            request.Client = client.Client_ID;
            client.SendMessage(CommandGenerator(request));
        }

        private void Mngr_ReceivedFile(ClientManager client, FileStandard message)
        {
            message.Sender = client.Client_ID.ToString();
            lstFiles.DisplayMember = "Name";
            lstFiles.Items.Add(message);
        }

        private void Host_Load(object sender, EventArgs e)
        {
            //Loads Addresses into CbIP
            CbIP.DataSource = Dns
                .GetHostEntry(SystemInformation.ComputerName)
                .AddressList
                .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                .ToList();
        }

        private void RelayMessage(object message)
        {
            //Loops through each User
            foreach (ClientManager c in Client_List)
                 c.SendMessage(message);
        }

        private CommandResult CommandGenerator(CommandRequest request)
        {
            //Builds the command result Object
            CommandResult ret = new CommandResult
            {
                User = request.Client
            };

            //Logs the user and the command
            lstCommands.Items.Add($"{request.Client}::{request.Message}");

            //Cleans the command from the message
            string command = request.Message.Split(' ')[0];
            command = command.Trim('!');

            //All commands exist within this switch (Scary i know)
            switch (command.ToLower())
            {
                //Needs to be manually updated
                case "help":
                    string[] commands = new string[]
                    {
                        "Help; displays this",
                        "list; returns a list of files on the server",
                        "clear; Clears the chat boxes",
                        "get <FileName>; pulls a specific file",
                        "download <FileName>; Downloads a file from a get",
                        "ping; pings the server",
                        "users; gets a list of active users"
                    };
                    ret.Contents = commands;
                    break;

                case "list":
                    List<string> files = new List<string>();
                    foreach (FileStandard a in lstFiles.Items) 
                        files.Add(a.Name);
                    ret.Contents = files.ToArray();
                    break;
                    
                case "get":
                    //This solves a bug when it comes to files with a space in the name
                    string File_Name = null;
                    for (int i = 1; i < request.Message.Split(' ').Length; i++)
                        File_Name += request.Message.Split(' ')[i] + " ";
                    File_Name = File_Name.TrimEnd();

                    for (int i = 0; i < lstFiles.Items.Count; i++)
                        if (((FileStandard)lstFiles.Items[i]).Name == File_Name)
                        {
                            ret.Contents = lstFiles.Items[i];
                            break;
                        }
                    break;
                    
                    //Returns a list of the current users
                case "users":
                    List<string> Usernames = new List<string>();
                    //Loops through all users and adds them to the Usernames
                    for(int i = 0; i < Client_List.Count; i++)
                    {
                        //
                        if(Client_List[i].Client_ID != (int)request.Client)
                        {
                            if (Client_List[i].Client_Name == null || string.IsNullOrWhiteSpace(Client_List[i].Client_Name))
                                Usernames.Add($"User ID: {Client_List[i].Client_ID}");
                            else
                                Usernames.Add(Client_List[i].Client_Name);
                        }
                    }
                    ret.Contents = Usernames.ToArray();
                    break;

                case "ping":
                    TimeSpan Pong = DateTime.Now - request.RequestTime;
                    ret.Contents = Pong;
                    break;

                    //Runs if no command was found
                default:
                    ret.Contents = "Command Does not exist!";
                    break;
            }
            //if nothing is found within the command this will be added
            if (ret.Contents == null)
                ret.Contents = "Nothing was found!";

            return ret;
        }

        private void WriteLog(string Message, EventLogEntryType Severity = EventLogEntryType.Information)
        {
            if (Severity != EventLogEntryType.Information)
                File.AppendAllText("ErrorLog", $"{Severity}::{Message}");
            lstError.Items.Add($"{Severity}::{Message}");
        }
    }
}
