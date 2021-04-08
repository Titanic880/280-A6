using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

using static Standards.DataProperties;
using Standards;

namespace A6_TCP_Client.Security
{
    /// <summary>
    /// Defines a Connected Client (Client side)
    /// </summary>
    public class ClientCommunication
    {
        //public const int port = 25565;
        public readonly string Client_name = SystemInformation.ComputerName;
        public string Server_IP { get; private set; }

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
        //private BinaryReader reader;
        private BinaryWriter writer;

        readonly private BackgroundWorker wkr = new BackgroundWorker();

        public ClientCommunication(string Serverip)
        {
            Server_IP = Serverip;

            wkr.WorkerReportsProgress = true;
            wkr.WorkerSupportsCancellation = true;
            
            wkr.DoWork += Wkr_DoWork;
            
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
            client.Connect(Server_IP, Port);
            nStream = client.GetStream();
            
            //Checks to see if the client or stream failed to init
            if (nStream == null || client == null)
                return;
            
            //Set up the read/write
            //reader = new BinaryReader(nStream);
            writer = new BinaryWriter(nStream);

            //Checks if we're connected
            Connected(Server_IP, Port);

            IFormatter formatter = new BinaryFormatter();
            //Main Loop checking if a new message was sent
            while (true)
            {
                if(nStream == null) //checks to see if the Stream is initilized
                    continue;

                object o = formatter.Deserialize(nStream);
                if (o == null) //Checks to see if nothing is found
                    continue;
                else if (o is string st) //Command support
                    ReceivedMessage(st);
                else if (o is byte[] v) //Command support
                    ReceivedFile(v);
            }
        }
    }
}
