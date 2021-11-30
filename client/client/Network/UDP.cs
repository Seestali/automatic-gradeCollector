using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace client.Network
{
    public class UDP
    {
        public const int PORT = 42069;
        public const string SERVER_DOMAIN = "vollsm.art";
        public bool packetReceived;

        private static UDP instance;
        private readonly UdpClient udpReceiver;
        private readonly UdpClient udpSender;
        private IPEndPoint remoteEP;
        private byte[] receivedBytes;

        private UDP(string serverDomain, int port)
        {
            udpReceiver = new UdpClient(port);
            udpSender = new UdpClient();
            remoteEP = new IPEndPoint(Dns.GetHostEntry(serverDomain).AddressList[0], port);
        }

        private UDP(IPAddress serverIP, int port)
        {
            udpReceiver = new UdpClient(port);
            udpSender = new UdpClient();
            remoteEP = new IPEndPoint(serverIP, port);
        }

        public static UDP GetInstance()
        {
            if (instance == null)
            {
                instance = new UDP(SERVER_DOMAIN, PORT);
            }
            return instance;
        }

        public void Send(byte[] datagram)
        {
            udpSender.Send(datagram, datagram.Length, remoteEP);
        }

        public void BeginReceive()
        {
            udpReceiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            while (!packetReceived)
            {
                Thread.Sleep(100);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            receivedBytes = udpReceiver.EndReceive(ar, ref remoteEP);
            Console.WriteLine($"Received: {myToString(receivedBytes)}");
            packetReceived = true;
        }

        private string myToString(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length);
            foreach (byte b in bytes)
            {
                stringBuilder.Append(b);
            }
            return stringBuilder.ToString();
        }
    }
}
