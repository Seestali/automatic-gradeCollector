using System.Collections.Generic;

namespace client.Network
{
    public class PacketManager
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

        public PacketManager()
        {
            packetList = new List<Packet>();
        }
    }
}