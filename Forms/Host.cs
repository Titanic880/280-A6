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
        }

        private void Mngr_NewClientConnected(ClientManager client)
        {
            //Adds the client to the List
            Client_List.Add(client);

            WriteLog($">>>>Client#{client.Client_ID} has connected");
            RelayMessage($">>>>Client#{client.Client_ID} has connected");

            NewClient();
        }

        private void Mngr_ClientDisconnected(ClientManager client)
        {
            if (Client_List.Remove(client))
            {
                RelayMessage($"{client.Client_ID} has disconnected!");
                WriteLog($"{client.Client_ID} has disconnected!");
            }
            else
                WriteLog($"{client.Client_ID} doesn't exist!", EventLogEntryType.Warning);   
        }

        private void Mngr_ReceivedMessage(ClientManager client, string message)
        {
            if (message.StartsWith("!"))    //Checks for command, if found it will generate a command File and send it
            {
                //Sends the command to the command Manager in the prebuilt format, and immediately returns to the user
                client.SendMessage(CommandGenerator(new CommandRequest
                {
                    Client = client,
                    Message = message
                }));
            }
            else
            {
                string msg = $">>>>{client.Client_ID}:{message}";
                lstUMessage.Items.Add(msg);
                RelayMessage(msg);
            }
        }

        private void Mngr_ReceivedFile(ClientManager client, FileStandard message)
        {
            message.Sender = client.Client_ID.ToString();
            RelayMessage(message);
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
            CommandResult ret = new CommandResult
            {
                User = request.Client
            };
            //Logs the user
            lstCommands.Items.Add($"{((ClientManager)request.Client).Client_ID}::{request.Message}");

            //Cleans the command from the message
            string command = request.Message.Split(' ')[0];
            command.Trim('!');

            //All commands exist within this switch (Scary i know); 
            //if i need more than 3 i will make a class to handle them
            switch (command.ToLower())
            {
                //!list
                case "list":
                    List<string> files = new List<string>();
                    foreach (FileStandard a in lstFiles.Items) 
                        files.Add(a.Name);
                    ret.Contents = files;
                    break;
                    //!get
                case "get":
                    for (int i = 0; i < lstFiles.Items.Count; i++)
                        if (((FileStandard)lstFiles.Items[i]).Name == request.Message.Split(' ')[1])
                            ret.Contents = lstFiles.Items[i];
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
