using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using client.Exception;
using client.Network;

namespace client.Network
{
    public class Manager
    {
        //TODO: class between UI and PacketAssembler.cs
        //TODO: consider relocation
        //TODO: manages user input on interface and instructs assembler class to build right packet
        //TODO: receives orders from UI and forwards constructions to packet assembler class
        //TODO: returns status / information of packets to UI
        //TODO: handle DEC packets and invalid packet
        //TODO: create unittest class
        //TODO: gives ready packets to UDP class

        private static Manager instance;
        private List<Form> forms;
        private Dictionary<uint, Tuple<Packet, long>> packets;
        private PacketAssembler packetAssembler;
        private string auth;

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
            forms = new List<Form>();
            packets = new Dictionary<uint, Tuple<Packet, ulong>>();
            packetAssembler = new PacketAssembler();
            OpenForm<Login>();
        }
        
        /// <summary>
        /// Opens Form by identification
        /// </summary>
        /// <typeparam name="T">Form identification</typeparam>
        public void OpenForm<T>() where T : Form, new()
        {
            new T().Show();
        }

        public void ReceivePseudo(byte[] array)
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
                case OpCode.LoginAns:
                    HandleLoginAns(packet);
                    break;
                case OpCode.SubjectsAndGradesAns:
                    HandleSubjectsAndGradesAns(packet);
                    break;
                case OpCode.SetGradesAns:
                    HandleSetGradesAns(packet);
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
            
        }

        public void HandleLoginAns(Packet packet)
        {
            
        }

        public void HandleSubjectsAndGradesAns(Packet packet)
        {
            
        }

        public void HandleSetGradesAns(Packet packet)
        {
            
        }

        public bool CheckIfFormIsOpen(string formname)
        {
            bool formOpen= Application.OpenForms.Cast<Form>().Any(form => form.Name == formname);
            return formOpen;
        }

        private void SendDeny(uint packetNumberToDeny, Error error)
        {
            Packet packet = packetAssembler.BuildDeny(packetNumberToDeny, error);
        }

        private void SendAck(uint packetNumberToAck)
        {
            Packet packet = packetAssembler.BuildAck(packetNumberToAck);
        }

        private void SendLoginReq(string email, string password)
        {
            Packet packet = packetAssembler.BuildLoginReq(email, password)
            AddPacketToDictionary(packet);
        }

        private void SendGetSubjectsAndGrades(int semester)
        {
            Packet packet = packetAssembler.BuildDeny(packetNumberToAck, error);
            AddPacketToDictionary(packet);
        }

        private void SendSetGrades(/* ... */)
        {
            Packet packet = packetAssembler.BuildDeny(packetNumberToAck, error);
            AddPacketToDictionary(packet);
        }

        private void AddPacketToDictionary(Packet packet)
        {
            OpCode opCode = OpCode.Ack;
            long millis = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Tuple<Packet, long> packetTuple = new Tuple<Packet, long>(packet, millis);
            packets.Add(packet.GetNumber(), packetTuple);
        }
    }
}