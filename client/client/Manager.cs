using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using client.Exception;
using client.Forms;
using client.Network;
using client.Utils;

namespace client
{
    //TODO: class between UI and PacketAssembler.cs
    //TODO: consider relocation
    //TODO: manages user input on interface and instructs assembler class to build right packet
    //TODO: receives orders from UI and forwards constructions to packet assembler class
    //TODO: returns status / information of packets to UI
    //TODO: handle DEC packets and invalid packet
    //TODO: create unittest class
    //TODO: gives ready packets to UDP class
    public class Manager
    {
        private const string HOSTNAME = "vollsm.art";
        //private const string MY_SMARTPHONE = "192.168.178.45";
        private const int PORT = 42069;
        private const int TEN_SECONDS = 10000;
        private const int TIMEOUT = 2000;
        private const byte TRUE = 1;

        private static Manager instance;
        private readonly Form[] forms;
        private readonly PacketAssembler packetAssembler;
        private readonly UdpClient udpClient;
        private readonly Tuple<UdpClient, IPEndPoint> udpServer;
        private readonly Dictionary<uint, Tuple<Packet, long>> packets;
        private readonly System.Timers.Timer timer;
        private Tuple<bool, string> auth;

        /// <summary>
        /// Singleton of Manager.
        /// </summary>
        /// <returns>Returns instance of active manager</returns>
        public static Manager GetInstance()
        {
            if (instance == null)
                instance = new Manager();
            return instance;
        }

        /// <summary>
        /// Private constructor of Manager.
        /// Used for Singleton.
        /// </summary>
        private Manager()
        {
            forms = new Form[] { new Login(), new MainWindow() };

            packets = new Dictionary<uint, Tuple<Packet, long>>();
            packetAssembler = new PacketAssembler();

            udpClient = new UdpClient(HOSTNAME, PORT);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, PORT);
            udpServer = new Tuple<UdpClient, IPEndPoint>(new UdpClient(ep), ep);
            udpServer.Item1.BeginReceive(new AsyncCallback(ReceiveCallback), udpServer);

            timer = new System.Timers.Timer(TEN_SECONDS);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            IPEndPoint ep = udpServer.Item2;
            byte[] bytes = udpServer.Item1.EndReceive(asyncResult, ref ep);
            udpServer.Item1.BeginReceive(new AsyncCallback(ReceiveCallback), udpServer);

            Console.WriteLine("\nPACKET RECEIVED!\n");
            PrintBytes(bytes);
            Console.WriteLine("\n");

            ReceivePacket(bytes);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            long totalSeconds = GetTotalSeconds();
            foreach (KeyValuePair<uint, Tuple<Packet, long>> packetKVPair in packets)
            {
                if (totalSeconds - packetKVPair.Value.Item2 > TIMEOUT)
                {
                    SendAgain(packetKVPair.Value.Item1);
                }
            }
        }

        public Form GetForm(CustomForms form)
        {
            return forms[(int)form];
        }

        public void SendDeny(uint packetNumberToDeny, Error error)
        {
            Packet packet = packetAssembler.BuildDeny(packetNumberToDeny, error);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent:");
            Console.WriteLine("PacketNumberToDeny={0}, Error={1}", packetNumberToDeny, error);
            PrintBytes(dgram);
        }

        public void SendAck(uint packetNumberToAck)
        {
            Packet packet = packetAssembler.BuildAck(packetNumberToAck);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent:");
            Console.WriteLine("PacketNumberToAck={0}", packetNumberToAck);
            PrintBytes(dgram);
        }

        public void SendLoginRequest(string eMail, string password)
        {
            string passwordHash = Hash.GetHashString(password);
            auth = new Tuple<bool, string>(false, eMail + "::" + passwordHash);
            Packet packet = packetAssembler.BuildLoginRequest(eMail, passwordHash);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent:");
            Console.WriteLine("E-Mail={0}, Password={1}, PasswordHash={2}", eMail, password, passwordHash);
            PrintBytes(dgram);
        }

        public void SendGetSubjectsAndGradesRequest(int semester)
        {
            Packet packet = packetAssembler.BuildGetSubjectsAndGradesRequest(auth.Item2, semester);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent:");
            Console.WriteLine("Auth={0}, Semester={1}", auth.Item2, semester);
            PrintBytes(dgram);
        }

        public void SendSetGradesRequest(/* ... */)
        {
            Packet packet = packetAssembler.BuildSetGradesRequest(auth.Item2/*, ... */);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent:");
            Console.WriteLine("Auth={0}", auth.Item2);
            PrintBytes(dgram);
        }

        private void SendAgain(Packet packet)
        {
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            Console.WriteLine("Packet sent again:");
            PrintBytes(dgram);
        }

        public void ReceivePacket(byte[] array)
        {
            Packet packet;
            try
            {
                packet = packetAssembler.DisassemblePacket(array);
            }
            catch (ChecksumMismatchException e)
            {
                SendDeny(e.GetPacketNumber(), Error.ChecksumMismatch);
                return;
            }

            switch (packet.GetOpCode())
            {
                case OpCode.Deny:
                    HandleDeny(packet);
                    break;
                case OpCode.Ack:
                    HandleAck(packet);
                    break;
                case OpCode.LoginAnswer:
                    HandleLoginAnswer(packet);
                    break;
                case OpCode.GetSubjectsAndGradesAnswer:
                    HandleSubjectsAndGradesAnswer(packet);
                    break;
                case OpCode.SetGradesAnswer:
                    HandleSetGradesAnswer(packet);
                    break;
                default:
                    // Ignore invalid op codes
                    break;
            }
        }

        public void HandleDeny(Packet packet)
        {

        }

        public void HandleAck(Packet packet)
        {
            packets.Remove(packet.GetNumber());
        }

        public void HandleLoginAnswer(Packet packet)
        {
            byte[] payloadData = packet.GetPayloadData();
            if (payloadData[0] == TRUE)
            {
                auth = new Tuple<bool, string>(true, auth.Item2);
            }
            else
            {
                ((Login)GetForm(CustomForms.Login)).SetErrorText("Login failed");
            }
            SendAck(packet.GetNumber());
            ((Login)GetForm(CustomForms.Login)).LoginVerified();
        }

        public void HandleSubjectsAndGradesAnswer(Packet packet)
        {
            SendAck(packet.GetNumber());
        }

        public void HandleSetGradesAnswer(Packet packet)
        {
            SendAck(packet.GetNumber());
        }

        private void AddPacketToDictionary(Packet packet)
        {
            long totalSeconds = GetTotalSeconds();
            Tuple<Packet, long> packetTuple = new Tuple<Packet, long>(packet, totalSeconds);
            packets.Add(packet.GetNumber(), packetTuple);
        }

        private long GetTotalSeconds()
        {
            TimeSpan timeSpan = DateTime.UtcNow - DateTime.MinValue;
            return (long)timeSpan.TotalSeconds;
        }

        private void PrintBytes(byte[] array)
        {
            string str = "";
            foreach (byte b in array)
            {
                str += b + " ";
            }
            Console.WriteLine(str);
        }
    }
}
