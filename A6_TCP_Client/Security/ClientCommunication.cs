using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A6_TCP_Client.Security
{
    /// <summary>
    /// Defines a Connected Client (Client side)
    /// </summary>
    public class ClientCommunication
    {
        public const int port = 25565;
        public readonly string Client_name = SystemInformation.ComputerName;
        public readonly string Server_name;

        //Connects to the server
        public event ConnectedEventHandler Connected;
        public delegate void ConnectedEventHandler(string servername, int port);

        //runs if the connection fails
        public event ConnectionFailedEventHandler ConnectionFailed;
        public delegate void ConnectionFailedEventHandler(string servername, int port);

        //Runs when message is recieved
        public event ReceivedMessageEventHandler ReceivedMessage;
        public delegate void ReceivedMessageEventHandler(string message);

        //Runs when a file is recieved
        public event ReceivedFileEventHandler ReceivedFile;
        public delegate void ReceivedFileEventHandler(byte[] message);

        //User Stack
        private TcpClient client;
        private NetworkStream nStream;
        private BinaryReader reader;
        private BinaryWriter writer;

        private BackgroundWorker wkr = new BackgroundWorker();

        public ClientCommunication(string Servername)
        {
            Server_name = Servername;

            wkr.WorkerReportsProgress = true;
            wkr.WorkerSupportsCancellation = true;
            
            wkr.DoWork += Wkr_DoWork;
            wkr.ProgressChanged += Wkr_ProgressChanged;

            wkr.RunWorkerAsync();
        }

        public void SendMessage(object message)
        {
            //Checks for any issues that might crash the program
            if (message == null || writer == null)
                return;

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writer.BaseStream, message);
        }

        private void Wkr_DoWork(object sender, DoWorkEventArgs e)
        {
            client = new TcpClient();

            //Conenects client to the server
            client.Connect(Server_name, port);
            nStream = client.GetStream();
            
            //Checks to see if the client or stream failed to init
            if (nStream == null || client == null)
                return;
            
            //Set up the read/write
            reader = new BinaryReader(nStream);
            writer = new BinaryWriter(nStream);

            //Checks if we're connected
            Connected(Server_name, port);

            //Main Loop checking if a new message was sent
            IFormatter formatter = new BinaryFormatter();
            while (true)
            {
                if(nStream == null) //checks to see if the Stream is initilized
                    continue;

                object o = formatter.Deserialize(nStream);
                if (o == null) //Checks to see if nothing is found
                    continue;
                if (o is string) //Command support
                    ReceivedMessage(o.ToString());
                if (o is byte[]) //Command support
                    ReceivedFile((byte[])o);
                
            }
        }
        /*
        private void Test()
        {
            while (true)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    object o = formatter.Deserialize(nStream);
                    switch (o.GetType())
                    {
                        case string:

                            break;

                        case byte[]:

                            break;
                    }
                }
                catch
                {

                }
            }

        }*/
        private void Original()
        {
            while (true)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    object o = formatter.Deserialize(nStream);
                    if (o is string) //Command support
                    {
                        ReceivedMessage(o.ToString());
                    }
                    if (o is byte[]) //Command support
                    {
                        ReceivedFile((byte[])o);
                    }
                }
                catch
                {

                }
            }
        }

        private void Wkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    //connect
                    break;
                case 1:
                    //message
                    break;
                case 2:
                    //disconnect
                    break;
                case 3:
                    //file
                    
                    break;
            }
        }
    }
}
