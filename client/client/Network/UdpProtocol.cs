using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace client.Network
{
    /// <summary>
    /// Short UDP-Protocol to manage communication between client and server.
    /// Does only work for data bursts, not constant streams.
    /// </summary>
    public class UdpProtocol
    {
        //public socket, otherwise usage can't read socket, maybe change to IDispsable, so _socket can be private
        public Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;   //declare bufferSize for receiving information into buffer
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0); // new EndPoint listening on all ports [port: 0].
        private AsyncCallback recv = null;  

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }
        /// <summary>
        /// Creating a server object for debug and testing purposes.
        /// Creating local server, listening on all ports.
        /// Socket will be bound to address and specific port at object creation.
        /// Answer from server can be on different port.
        /// </summary>
        /// <param name="address"> Server Address as IP-Address. DNS will not be resolved</param>
        /// <param name="port"> Server port to connect to / to open for incoming packets</param>
        //function 
        public void Server(string address, int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true); //socket configurations for async multiple use of port.
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));   //binding IP-Address and port to IPEndPoint
            Receive();  //start listening for incoming packets (async)          
        }
        /// <summary>
        /// Creating a Client object for sending and receiving packets from server.
        /// Is used for sending credentials, Semester and student information.
        ///
        /// Used as following:
        /// 
        /// UdpProtocol c = new UdpProtocol();
        /// c.Client("178.203.36.119", 42069);
        /// c.Send("Hallo Henny");
        /// </summary>
        /// <param name="address"> Address as IP-Address. Address to which the client will connect. DNS will not be resolved.</param>
        /// <param name="port">Port to which the client will try to connect when sending a UDP package.</param>
        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);    //connects socket to specific IP-Address and port
            Receive();  //start async receiving packets from server        
        }
        /// <summary>
        /// "Send"-Function for Client of Server to send UDP packets to given IP-Address and given port.
        /// Sending and receiving is configured as "async".
        /// Sends data, in "BeginSend" --> callback: "ar" as AsyncResult in State "so".
        ///
        /// Used as call from client or server object.
        /// </summary>
        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                System.Diagnostics.Debug.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }
        /// <summary>
        /// "Receive"-Function for Client of Server to send UDP packets to given IP-Address and given port.
        /// Sending and receiving is configured as "async".
        /// Begins receiving from epFrom to buffer.
        /// Saves to bytes and prints to debug console.
        ///
        /// "Receive" does not need to be called from object.
        /// Async listening is always enabled. 
        /// </summary>
        private void Receive()
        {
            System.Diagnostics.Debug.WriteLine("Started Receiving");
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                System.Diagnostics.Debug.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
            }, state);
        }
    }
}
