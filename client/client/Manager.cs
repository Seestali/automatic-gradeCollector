﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Manager
    {
        private const string HOSTNAME = "vollsm.art";
        private const int PORT = 42069;
        private const int TEN_SECONDS = 10000;
        private const int TIMEOUT = 2000;

        private static Manager instance;
        private readonly Form[] forms;
        private readonly PacketAssembler packetAssembler;
        private IPEndPoint ep;
        private readonly UdpClient udpClient;
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

            ep = new IPEndPoint(IPAddress.Any, PORT);
            udpClient = new UdpClient(HOSTNAME, PORT);
            udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);

            timer = new System.Timers.Timer(TEN_SECONDS);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            byte[] bytes = udpClient.EndReceive(asyncResult, ref ep);
            udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
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
            PrintPacket(packet, "sent");
        }

        public void SendAck(uint packetNumberToAck)
        {
            Packet packet = packetAssembler.BuildAck(packetNumberToAck);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            PrintPacket(packet, "sent");
        }

        public void SendLoginRequest(string eMail, string password)
        {
            string passwordHash = Hash.GetHashString(password);
            auth = new Tuple<bool, string>(false, eMail + "::" + passwordHash);
            Packet packet = packetAssembler.BuildLoginRequest(eMail, passwordHash);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            PrintPacket(packet, "sent");
        }

        public void SendGetSubjectsAndGradesRequest(int semester)
        {
            Packet packet = packetAssembler.BuildGetSubjectsAndGradesRequest(auth.Item2, semester);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            PrintPacket(packet, "sent");
        }

        public void SendSetGradesRequest(/* ... */)
        {
            Packet packet = packetAssembler.BuildSetGradesRequest(auth.Item2/*, ... */);
            AddPacketToDictionary(packet);
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            PrintPacket(packet, "sent");
        }

        private void SendAgain(Packet packet)
        {
            byte[] dgram = packet.ToByteArray();
            udpClient.Send(dgram, dgram.Length);
            PrintPacket(packet, "sent again");
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

            PrintPacket(packet, "received");

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
            byte[] payload = packet.GetPayloadData();
            switch ((Error)payload[1])
            {
                case Error.AuthFailed:
                    ((Login)GetForm(CustomForms.Login)).SetErrorText("Ungülte E-Mail-Adresse oder Passwort.");
                    break;
                case Error.ChecksumMismatch:
                    uint packetNumber = ByteUtil.GetUInt32FromByteArray(payload, 0);
                    Tuple<Packet, long> tuple;
                    packets.TryGetValue(packetNumber, out tuple);
                    Packet packetToSendAgain = tuple.Item1;
                    SendAgain(packetToSendAgain);
                    break;
                case Error.PayloadInvalid:
                    // TODO: unhandled
                    break;
            }
            packets.Remove(packet.GetNumber());
        }

        public void HandleAck(Packet packet)
        {
            packets.Remove(packet.GetNumber());
        }

        public void HandleLoginAnswer(Packet packet)
        {
            ((Login)GetForm(CustomForms.Login)).LoginVerified();
            SendAck(packet.GetNumber());
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

        private void PrintPacket(Packet packet, string sentReceived)
        {
            switch (sentReceived)
            {
                case "received":
                    Debug.WriteLine("Packet received:");
                    break;
                case "sent":
                    Debug.WriteLine("Packet sent:");
                    break;
                case "sent again":
                    Debug.WriteLine("Packet sent again:");
                    break;
                default:
                    break;
            }
            Debug.WriteLine("PacketNr={0} UserID={1} OpCode={2} CRC={3} PayloadLength={4}", 
                packet.GetNumber(), packet.GetUserID(), packet.GetOpCode(), packet.GetCRC(), packet.GetPayLoadLength());
            Debug.WriteLine(ToString(packet.ToByteArray()) + Environment.NewLine);
        }

        private string ToString(byte[] array)
        {
            string str = "";
            foreach (byte b in array)
            {
                str += b + " ";
            }
            return str;
        }
    }
}
