using System.Collections.Generic;
using A6_TCP_Client.Security;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Data;
using System.Net;
using System.IO;
using System;

namespace A6_TCP_Client
{
    public partial class Client : Form
    {
        /// <summary>
        /// Queue of messages
        /// </summary>
        readonly Queue<string> IncomingMessages = new Queue<string>();
        readonly BackgroundWorker msgwkr = new BackgroundWorker();
        ClientCommunication Client_Comms;

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
            Client_Comms.ReceivedMessage += Client_Comms_ReceivedMessage;
            Client_Comms.ReceivedFile += Client_Comms_ReceivedFile;
        }

        private void Serv_Connected(string servername, int port)
        {
            string incomingConnectionMessage = $">>>{servername}@{port}connected";
            IncomingMessages.Enqueue(incomingConnectionMessage);
        }

        private void Client_Load(object sender, EventArgs e)
            => CbIP.DataSource = Dns.GetHostEntry(SystemInformation.ComputerName).AddressList
               .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList();

        private void Client_Comms_ReceivedMessage(string message)
        {
            IncomingMessages.Enqueue(message);
            BeginInvoke(new MethodInvoker(DisplayMessages));
        }

        private void Client_Comms_ReceivedFile(byte[] message)
            => File.WriteAllBytes("TestFileToWrite.txt", message);

        private void BtnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Choose a file to upload"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Standards.FileStandard fileToSend = new Standards.FileStandard(ofd.FileName, File.ReadAllBytes(ofd.FileName));
                Client_Comms.SendMessage(fileToSend);
                MessageBox.Show("File Sent!");
            }
        }
    }
}
