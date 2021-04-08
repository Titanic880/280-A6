using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using A6_TCP_Client.Security;

namespace A6_TCP_Client
{
    public partial class Client : Form
    {

        ClientCommunication Client_Comms;
        /// <summary>
        /// Queue of messages
        /// </summary>
        Queue<string> IncomingMessages = new Queue<string>();

        BackgroundWorker msgwkr = new BackgroundWorker();

        public Client()
        {
            InitializeComponent(); 
            msgwkr.DoWork += Msgwkr_DoWork;
            msgwkr.RunWorkerAsync();
        }

        private void Msgwkr_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IncomingMessages.Count > 0)
                lstUMessage.Items.Add(IncomingMessages.Dequeue());

        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            //Gets the message and sets TB to null (Empty)
            string msg = TbMessage.Text;
            TbMessage.Text = null;
            Client_Comms.SendMessage(msg);
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            lstUMessage.Items.Clear();
            Client_Comms.SendMessage($">>User {Client_Comms.Client_name} has Disconnected!");
            Client_Comms = null;
        }

        private void DisplayMessages()
        {
            while (IncomingMessages.Count > 0)
            {
                string tmp;
                tmp = IncomingMessages.Dequeue();
                lstUMessage.Items.Add(tmp);
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Client_Comms = new ClientCommunication(CbIP.Text);
            Client_Comms.Connected += Serv_Connected;
            Client_Comms.ConnectionFailed += Serv_ConnectionFailed;
            Client_Comms.ReceivedMessage += Client_Comms_ReceivedMessage;
            Client_Comms.ReceivedFile += Client_Comms_ReceivedFile;
        }



        private void Serv_Connected(string servername, int port)
        {
            string incomingConnectionMessage = $">>>{servername}@{port}connected";
            IncomingMessages.Enqueue(incomingConnectionMessage);
        }
        private void Serv_ConnectionFailed(string server_ip, int port)
        {
            MessageBox.Show($"Failed to connect to {server_ip}:{port}");
        }

        private void Client_Load(object sender, EventArgs e)
        {
            //Loads the combo box with current users ip (Change to a list from file)
            CbIP.DataSource = Dns
                .GetHostEntry(SystemInformation.ComputerName)
                .AddressList
                .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToList();
        }
        /// <summary>
        /// Release version of the client Load (Loads to and from file for ip's)
        /// </summary>
        private void Client_Load_Release()
        {
            if (!File.Exists("Host_List"))
            {
                IPAddress[] lst = Dns
                    .GetHostEntry(SystemInformation.ComputerName)
                    .AddressList
                    .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .ToArray();


                //File.WriteAllBytes("Host_List");
            }
        }

        private void Client_Comms_ReceivedMessage(string message)
        {
            IncomingMessages.Enqueue(message);
            this.BeginInvoke(new MethodInvoker(DisplayMessages));
        }

        private void Client_Comms_ReceivedFile(byte[] message)
        {
            File.WriteAllBytes("TestFileToWrite.txt", message);
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            byte[] fileToSend = File.ReadAllBytes("TestFileToSend.txt");
            Client_Comms.SendMessage(fileToSend);
        }
    }
}
