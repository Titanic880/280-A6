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

namespace A6_TCP_Client
{
    public partial class Client : Form
    {
        Security.ClientCommunication Client_Comms;
        Queue<string> msgQ = new Queue<string>();

        BackgroundWorker msgwkr = new BackgroundWorker();

        public Client()
        {
            InitializeComponent(); 
            msgwkr.DoWork += Msgwkr_DoWork;
            msgwkr.RunWorkerAsync();
        }

        private void Msgwkr_DoWork(object sender, DoWorkEventArgs e)
        {
            while (msgQ.Count > 0)
                lstUMessage.Items.Add(msgQ.Dequeue());

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
            //Disconnects from current host
        }



        private void DisplayMessages()
        {
            while (msgQ.Count > 0)
            {
                string tmp;
                tmp = msgQ.Dequeue();
                lstUMessage.Items.Add(tmp);
            }
            
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Client_Comms = new Security.ClientCommunication(CbIP.Text);
            Client_Comms.Connected += Serv_Connected;
            Client_Comms.ConnectionFailed += Serv_ConnectionFailed;
            Client_Comms.ReceivedMessage += Client_Comms_ReceivedMessage;
            Client_Comms.ReceivedFile += Client_Comms_ReceivedFile;
        }



        private void Serv_Connected(string servername, int port)
        {
            string incomingConnectionMessage = $">>>>{servername}@{port}connected";
            msgQ.Enqueue(incomingConnectionMessage);
        }
        private void Serv_ConnectionFailed(string servername, int port)
        {
            throw new NotImplementedException();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            CbIP.DataSource = Dns
                .GetHostEntry(SystemInformation.ComputerName)
                .AddressList
                .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .ToList();
        }
        private void Client_Comms_ReceivedMessage(string message)
        {
            msgQ.Enqueue(message);
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
