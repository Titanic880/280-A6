using System.Collections.Generic;
using A6_TCP_Client.Security;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Data;
using System.Net;
using Standards;
using System.IO;
using System;

namespace A6_TCP_Client
{
    public partial class Client : Form
    {
        /// <summary>
        /// Queue of messages
        /// </summary>
        readonly Queue<object> IncomingMessages = new Queue<object>();
        ClientCommunication Client_Comms;
        public Client() => InitializeComponent();

        #region Buttons
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

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Client_Comms = new ClientCommunication(CbIP.Text);
            Client_Comms.ReceivedMessage += Client_Comms_ReceivedMessage;
            Client_Comms.ReceivedFile += Client_Comms_ReceivedFile;
            Client_Comms.Connected += Serv_Connected;
        }
        #endregion

        private void DisplayMessages()
        {
            while (IncomingMessages.Count > 0)
            {
                object o = IncomingMessages.Dequeue();
                if (o is FileStandard)
                {
                    lstFiles.DisplayMember = "Name";
                    lstFiles.Items.Add(o);
                }
                else
                    lstUMessage.Items.Add(o);
            }
        }
        #region Delegates
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
        #endregion Delegates

        #region File Handling
        private void Client_Comms_ReceivedFile(FileStandard message)
        {
            //lstFiles.Items.Add(message);
            IncomingMessages.Enqueue(message);
            BeginInvoke(new MethodInvoker(DisplayMessages));
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Choose a file to upload"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStandard fileToSend = new FileStandard(ofd.FileName.Split('\\')[ofd.FileName.Split('\\').Length-1], File.ReadAllBytes(ofd.FileName));
                Client_Comms.SendMessage(fileToSend);
            }
        }

        private void TbDownload_Click(object sender, EventArgs e)
        {
            if(lstFiles.SelectedItem is FileStandard standard)
                File.WriteAllBytes("Local Download.txt", standard.File);
        }
        #endregion
    }
}
