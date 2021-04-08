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
        TcpListener listener;
        ClientManager mngr;
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
            string msg = $">>>>{client.Client_ID}:{message}";
            lstUMessage.Items.Add(msg);
            RelayMessage(msg);
        }

        private void Mngr_ReceivedFile(ClientManager client, FileStandard message)
        {
            string recievedFileMessage = $"New file recieved from:{client.Client_ID}";
            RelayMessage(recievedFileMessage);
            RelayMessage(message);
            lstFiles.Name = "Name";
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

        private void WriteLog(string Message, EventLogEntryType Severity = EventLogEntryType.Information)
        {
            if (Severity != EventLogEntryType.Information)
                File.AppendAllText("ErrorLog", $"{Severity}::{Message}");
            lstError.Items.Add($"{Severity}::{Message}");
        }
    }
}
