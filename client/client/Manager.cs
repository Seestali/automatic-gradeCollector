using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        private List<Packet> packetList;
        private PacketAssembler packetAssembler;

        private static Manager instance;

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
            packetList = new List<Packet>();
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
            Packet packet = packetAssembler.DisassemblePacket(array);
            // ... Fehler? try catch
            switch (packet.GetOpCode())
            {
                case OpCode.Deny:
                    // ...
                    break;
                case OpCode.Ack:
                    // ...
                    break;
                case OpCode.LoginAns:
                    // ...
                    break;
                case OpCode.SubjectsAndGradesAns:
                    // ...
                    break;
                case OpCode.SetGradesAns:
                    // ...
                    break;
                default:
                    break;
            }
        }
    }
}