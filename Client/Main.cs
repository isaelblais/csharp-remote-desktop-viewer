using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Main : Form
    {
        private TcpListener connListener;
        private int connListenerPort = 7000;

        private TcpListener imgListener;
        private int imgListenerPort = 7001;

        private TcpClient client;
        private TcpClient imgClient;
        private Thread imageListenThread;
        private Thread listenerThread;

        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void ListenConnection()
        {
            connListener = new TcpListener(IPAddress.Any, connListenerPort);
            connListener.Start();
            // Starts listening for connections.
            while (true)
            {
                client = connListener.AcceptTcpClient();
                // After it gets accepted, it breaks the loop since we don't need it anymore.
                break;
            }
            lblConnected.Text = "Connected: " + client.Client.RemoteEndPoint.ToString();
            imageListenThread = new Thread(ListenImageReceive);
            // Sets the new thread to Deserialize the images.
            imageListenThread.Start();
        }

        private void ListenImageReceive()
        {
            imgListener = new TcpListener(IPAddress.Any, imgListenerPort);
            imgListener.Start();
            // Starts the imgListener.
            while (true)
            {
                imgClient = imgListener.AcceptTcpClient();
                // Same like above.
                break;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            while (true)
            {
                // Starts the loop.
                using (NetworkStream stream = imgClient.GetStream())
                {
                    // We're using the stream the image is serialized-
                    // to.
                    while (true)
                        // Gets the image and loops.
                        picDisplay.Image = (Image)formatter.Deserialize(stream);
                }
            }
        }

        private void BtnListen_Click(object sender, EventArgs e)
        {
            listenerThread = new Thread(ListenConnection);
            listenerThread.Start();
        }
    }
}
