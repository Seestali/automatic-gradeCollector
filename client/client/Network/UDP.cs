using System;
using System.Net;
using System.Net.Sockets;

namespace client.Network
{
    public class UDP
    {
        private static int port = 42069;
        private static string serverAddress = "";
        private static UdpClient udpReceiver = new UdpClient(port);
        private static IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverAddress), port);

        private UDP() { }

        public void Send(byte[] datagram)
        {
            using (UdpClient c = new UdpClient(port))
                c.SendAsync(datagram, datagram.Length, serverEP);
        }

        public void Receive()
        {
            udpReceiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            byte[] received = udpReceiver.EndReceive(result, ref serverEP);
            udpReceiver.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            // TODO: Received data must be forwarded
        }
    }
}
