using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Net.Sockets;
using Standards;
using System.IO;
using System;

namespace A6_TCP.Security
{
    /// <summary>
    /// Defines a Connected Client (Server side)
    /// </summary>
    public class ClientManager
    {
        //Stalker of the port
        public static TcpListener Client_Listener;

        //Client ID
        public static int ClientCounter;
        public string Client_Name;
        public int Client_ID;

        //Runs when a client first connects
        public event NewClientConnectedEventHandler NewClientConnected;
        public delegate void NewClientConnectedEventHandler(ClientManager client);

        //Runs when a client disconnects
        public event ClientDisconnectedEventHandler ClientDisconnected;
        public delegate void ClientDisconnectedEventHandler(ClientManager client);

        //Handles user messages
        public event ReceivedMessageEventHandler ReceivedMessage;
        public delegate void ReceivedMessageEventHandler(ClientManager client, string message);

        //Handles user files
        public event ReceivedFileEventHandler ReceivedFile;
        public delegate void ReceivedFileEventHandler(ClientManager client, FileStandard message);

        //Handles commands
        public event RecievedCommandReq RecievedCommand;
        public delegate void RecievedCommandReq(ClientManager client, CommandRequest request);

        //Sets the string username
        public event UserNameSet SetUserName;
        public delegate void UserNameSet(ClientManager UserName);

        //Main worker
        private readonly BackgroundWorker wkr = new BackgroundWorker();
        //Generic Socket
        private Socket C_Socket;
        //network datapath
        private NetworkStream C_nStream;
        //Generic read/write
        private BinaryReader C_reader;
        private BinaryWriter C_writer;

        public ClientManager(TcpListener listener)
        {
            //Sets Current client ID
            Client_ID = ClientCounter++;
            Client_Listener = listener;

            //Configures worker
            wkr.WorkerSupportsCancellation = true;
            wkr.WorkerReportsProgress = true;
            wkr.DoWork += Wkr_DoWork;
            wkr.ProgressChanged += Wkr_ProgressChanged;

            //Runs worker
            wkr.RunWorkerAsync();
        }

        public void SendMessage(object Msg)
        {
            if (Msg == null)
                return;

            else if (C_writer == null)  //Checks for writer, if its null wait a second for initilization attempt
            {
                System.Threading.Thread.Sleep(1000);
                if (C_writer == null)
                    return;
            }

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(C_writer.BaseStream, Msg);
        }

        #region Worker
        private void Wkr_DoWork(object sender, DoWorkEventArgs e)
        {
            //Makes sure the socket is connected
            C_Socket = Client_Listener.AcceptSocket();
            //Creates the user Connection objects
            wkr.ReportProgress(0);
            
            if (C_Socket == null)
                return;

            C_nStream = new NetworkStream(C_Socket);
            if (C_nStream == null)
                return;

            C_reader = new BinaryReader(C_nStream);
            C_writer = new BinaryWriter(C_nStream);

            IFormatter formatter = new BinaryFormatter(); //Moved out of loop (Memory leak inside)

            while (true)
            {
                //Error checking
                if (C_reader == null)
                    return;
                else if (C_reader.BaseStream == null)
                    return;

                //Distrobution of information
                object o = formatter.Deserialize(C_reader.BaseStream);
                if (o is null)
                    continue;
                else if (o is string st) //Command support
                    wkr.ReportProgress(1, st);
                else if (o is FileStandard v) //Command support
                    wkr.ReportProgress(3, v);
                else if (o is DisconnectUser DCUser)
                    wkr.ReportProgress(2, DCUser);
                else if (o is ConnectUser CUser)
                    wkr.ReportProgress(4, CUser);
                else if (o is CommandRequest CmdReq)
                    wkr.ReportProgress(5, CmdReq);
            }
        }

        private void Wkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    NewClientConnected(this);
                    break;
                case 1:
                    ReceivedMessage(this, e.UserState.ToString());
                    break;
                case 2:
                    //Disconnect
                    ClientDisconnected(this);
                    break;
                case 3:
                    ReceivedFile(this, (FileStandard)e.UserState);
                    break;
                case 4:
                    this.Client_Name = ((ConnectUser)e.UserState).Name;
                    SetUserName(this);
                    break;
                case 5:
                    RecievedCommand(this, (CommandRequest)e.UserState);
                    break;
            }
        }
        #endregion Worker
    }
}
