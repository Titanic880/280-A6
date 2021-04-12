using System.Collections.Generic;
using A6_TCP_Client.Security;
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
        private bool FileOverride = false;

        //Message history system
        private readonly List<string> messages = new List<string>();
        private int messagesPOS = 0;
        
        public Client()
        {
            InitializeComponent();
            KeyPreview = true;
            
            //Adds a blank slate for the message history
            messages.Add("");
        }
        #region Buttons
        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendToServer();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (BtnConnect.Text == "Connect")   //Connect button
            {
                BtnConnect.Text = "Disconnect";
                TbUsername.Enabled = false;
                CbIP.Enabled = false;

                Client_Comms = new ClientCommunication(CbIP.Text);

                Client_Comms.ReceivedMessage += Client_Comms_ReceivedMessage;
                Client_Comms.ReceivedFile += Client_Comms_ReceivedFile;
                Client_Comms.CommandRes += Client_Command_Result;
                Client_Comms.Connected += Serv_Connected;

                if (TbUsername.Text.Trim() != null && !string.IsNullOrWhiteSpace(TbUsername.Text))
                {
                    System.Threading.Thread.Sleep(1000);
                    Client_Comms.SendMessage(new ConnectUser(TbUsername.Text));
                }
            }
            else if (BtnConnect.Text == "Disconnect")    //Disconnect button
            {
                BtnConnect.Text = "Connect";
                TbUsername.Enabled = true;
                CbIP.Enabled = true;

                lstUMessage.Items.Clear();
                Client_Comms.SendMessage($">>User {Client_Comms.Client_name} has Disconnected!");
                Client_Comms.SendMessage(new DisconnectUser(Client_Comms.Client_name));
                Client_Comms = null;
            }
            else
                BtnConnect.Text = "Disconnect";
        }
        #endregion

        /// <summary>
        /// Displays the object recieved from the server
        /// </summary>
        private void DisplayMessages()
        {
            while (IncomingMessages.Count > 0)
            {
                object o = IncomingMessages.Dequeue();
                if (o is CommandResult result)
                    lstFiles.Items.Add(result.Contents);
                else if (o is FileStandard FS)
                {

                    if (FileOverride)
                        for (int i = 0; i < lstFiles.Items.Count; i++)
                            if ((string)lstFiles.Items[i] == FS.Name) //Finds and replaces string
                            {
                                lstFiles.Items.Remove(FS.Name);
                                lstFiles.DisplayMember = "Name";
                                lstFiles.Items.Insert(i, FS);
                                MessageBox.Show($"{FS.Name} has been downloaded!");
                                FileOverride = false;
                                break;
                            }
                            else
                            {
                                lstFiles.DisplayMember = "Name";
                                lstFiles.Items.Add(FS);
                            }
                }
                else if (o is FileStandard[] ar)
                {
                    lstFiles.Items.Clear();
                    foreach (FileStandard a in ar)
                    {
                        lstFiles.DisplayMember = "Name";
                        lstFiles.Items.Add(a);
                    }
                }
                else if (o is string[] StrArr)
                {
                    foreach (string a in StrArr)
                        lstFiles.Items.Add(a);
                }
                else if (o is TimeSpan pong)
                    lstFiles.Items.Add($"Pong! (Time taken: {pong})");
                else
                    lstUMessage.Items.Add(o);
            }
        }
        #region Delegates

        private void Client_Load(object sender, EventArgs e)
            => CbIP.DataSource = Dns.GetHostEntry(SystemInformation.ComputerName).AddressList
               .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList();
        private void Serv_Connected(string servername, int port)
            => UI_ADD($">>>{servername}@{port}connected");
        private void Client_Comms_ReceivedMessage(string message)
            => UI_ADD(message);
        private void Client_Command_Result(CommandResult results)
            => UI_ADD(results.Contents);
        #endregion Delegates

        #region File Handling
        private void Client_Comms_ReceivedFile(FileStandard message)
            => UI_ADD(message);
        

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
            if (lstFiles.SelectedItem is FileStandard standard)
                File.WriteAllBytes("Local Download.txt", standard.File);
            //Grabs the file
            else if (lstFiles.SelectedItem is string)
            {
                FileOverride = true;
                Client_Comms.SendMessage($"!get {lstFiles.SelectedItem}");
            }
        }
        #endregion
        #region Methods
        private void UI_ADD(object message)
        {
            IncomingMessages.Enqueue(message);
            BeginInvoke(new MethodInvoker(DisplayMessages));
        }

        /// <summary>
        /// Sends item to server 
        /// </summary>
        private void SendToServer()
        {
            if (Client_Comms != null)
            {
                //Gets the message and sets TB to null (Empty)
                string msg = TbMessage.Text;
                TbMessage.Text = null;

                messages.Add(msg);
                messagesPOS = 0;
                //Builds and sends a command
                if (msg.StartsWith("!"))
                {
                    CommandRequest req = new CommandRequest
                    { Message = msg };
                    Client_Comms.SendMessage(req);
                }
                else
                    Client_Comms.SendMessage(msg);
            }
        }
        #endregion
        private void TbMessage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Checks if the connect is made
            if (Client_Comms != null)
            {
                if (messagesPOS < 0) //Loops to top
                    messagesPOS = messages.Count;
                else if (messagesPOS > messages.Count) //Loops to bottom
                    messagesPOS = -1;

                //Switch of all used keys
                switch (e.KeyCode)
                {
                    //Cycles to older message
                    case Keys.Up:
                        messagesPOS++;

                        if (messagesPOS < 0) //Loops to top
                            messagesPOS = messages.Count - 1;
                        else if (messagesPOS >= messages.Count) //Loops to bottom
                            messagesPOS = 0;

                        if (messages[messagesPOS] == null)
                            return;
                        else
                            TbMessage.Text = messages[messagesPOS];
                        break;
                    //Cycles to newer message
                    case Keys.Down:
                        messagesPOS--;

                        if (messagesPOS < 0) //Loops to top
                            messagesPOS = messages.Count - 1;
                        else if (messagesPOS > messages.Count) //Loops to bottom
                            messagesPOS = 0;

                        if (messages[messagesPOS] == null)
                            return;
                        else
                            TbMessage.Text = messages[messagesPOS];
                        break;

                    //Sends a message if enter is pressed
                    case Keys.Enter:
                        SendToServer();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
