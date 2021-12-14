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

        public static Manager getInstance()
        {
            if (instance == null)
                instance = new Manager();
            return instance;
        }
        
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
    }
}