using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Linq;
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
        public int Client_ID;

        string Latest;

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
        public delegate void ReceivedFileEventHandler(ClientManager client, byte[] message);

        //Main worker
        private BackgroundWorker wkr = new BackgroundWorker();
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
            //Names thread (easier to debug)
            //System.Threading.Thread.CurrentThread.Name = "Client: " + Client_ID;
            
            //Sets the socket
            //C_Socket = Client_Listener.AcceptSocket();

            //Configures worker
            wkr.WorkerSupportsCancellation = true;
            wkr.WorkerReportsProgress = true;
            wkr.DoWork += Wkr_DoWork;
            wkr.ProgressChanged += Wkr_ProgressChanged;
            wkr.RunWorkerCompleted += Wkr_RunWorkerCompleted;

            //Runs worker
            wkr.RunWorkerAsync();
        }

        public void SendMessage(object Msg)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(C_writer.BaseStream, Msg);
            }
            catch
            {

            }
        }

        #region Worker
        private void Wkr_DoWork(object sender, DoWorkEventArgs e)
        {
            
            try //KILL THIS
            {
                //Makes sure the socket is connected
                C_Socket = Client_Listener.AcceptSocket();
                //Creates the user Connection objects
                wkr.ReportProgress(0);
                C_nStream = new NetworkStream(C_Socket);
                C_reader = new BinaryReader(C_nStream);
                C_writer = new BinaryWriter(C_nStream); 
                
                IFormatter formatter = new BinaryFormatter(); //Moved out of loop (Memory leak inside)
                while (true)
                {
                    try
                    {
                        object o = formatter.Deserialize(C_reader.BaseStream);
                        if (o is string) //Command support
                        {
                            Latest = o.ToString();
                            wkr.ReportProgress(1, Latest);
                        }
                        if (o is byte[]) //Command support
                        {
                            wkr.ReportProgress(3, (byte[])o);
                        }
                    }
                    catch
                    {

                    }
                }
            
            }
            catch(Exception ex)
            {
                throw ex;
                //Send event to Log
            }
        }

        //Trying to follow what derek had in class
        private void Wkr1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool Done = false;

            if (C_Socket == null)   //Makes sure the socket is connected
                C_Socket = Client_Listener.AcceptSocket();

            C_nStream = new NetworkStream(C_Socket);
            C_reader = new BinaryReader(C_nStream);
            C_writer = new BinaryWriter(C_nStream);
            IFormatter formatter = new BinaryFormatter();

            wkr.ReportProgress(0);

            while (!Done)
            {
                object o = formatter.Deserialize(C_reader.BaseStream);
                
                if(o != null)
                    wkr.ReportProgress(1, o);
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
                    ReceivedMessage(this,e.UserState.ToString());
                    break;
                case 2:
                    //Disconnect
                    break;
                case 3:
                    ReceivedFile(this,(byte[])e.UserState);
                    break;
            }
        }

        private void Wkr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion Worker
    }
}
