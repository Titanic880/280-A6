using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System;
using System.IO;

namespace A6_TCP.Forms
{
    public partial class Host : Form
    {
        TcpListener listener;
        Security.ClientManager mngr;

        public Host()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //Grabs and stores the current computers' IP
            IPAddress servername = (IPAddress)CbIP.SelectedValue;

            //Makes the listener
            listener = new TcpListener(servername, Framework.DataInfo.port);
            listener.Start();

            WriteLog($"Server Started on {servername}:{Framework.DataInfo.port}");

            NewClient();
        }

        private void NewClient()
        {
            //Builds a new Manager (Memory leaks?)
            mngr = new Security.ClientManager(listener);
            mngr.NewClientConnected += Mngr_NewClientConnected;
            mngr.ClientDisconnected += Mngr_ClientDisconnected;
            mngr.ReceivedMessage += Mngr_ReceivedMessage;
            mngr.ReceivedFile += Mngr_ReceivedFile;
        }

        private void Mngr_NewClientConnected(Security.ClientManager client)
        {
            //Adds the client to the List
            Framework.DataInfo.Client_List.Add(client);
            WriteLog($">>>>Client#{client.Client_ID} has connected");

            //Builds a new Client Manager (Memory leaks?)
            RelayMessage($">>>>Client#{client.Client_ID} has connected");
            NewClient();
        }

        private void Mngr_ClientDisconnected(Security.ClientManager client)
        {
            //Framework.DataInfo.Client_List.RemoveAt(client.Client_ID);
        }

        private void Mngr_ReceivedMessage(Security.ClientManager client, string message)
        {
            string msg = $">>>>{client.Client_ID}:{message}";
            lstUMessage.Items.Add(msg);
            RelayMessage(msg);
        }

        private void Mngr_ReceivedFile(Security.ClientManager client, byte[] message)
        {
            string recievedFileMessage = $"New file recieved from:{client.Client_ID}";
            RelayMessage(recievedFileMessage);
            RelayMessage(message);
            File.WriteAllBytes("testfile", message);
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            foreach (Security.ClientManager c in Framework.DataInfo.Client_List)
                c.SendMessage(message);
        }

       /// <summary>
       /// Writes to log Listbox
       /// </summary>
       /// <param name="Message"></param>
       /// <param name="Severity"></param>
        private void WriteLog(string Message, EventLogEntryType Severity = EventLogEntryType.Information)
        {
            lstUMessage.Items.Add(Message);
            lstUMessage.SelectedIndex = lstUMessage.Items.Count - 1;
        }
    }
}
