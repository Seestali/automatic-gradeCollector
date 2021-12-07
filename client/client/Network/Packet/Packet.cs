using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Network.Packet
{
    /// <summary>
    /// 
    /// </summary>
    public class Packet
    {
        private readonly uint number;
        private readonly byte userID;
        private readonly OpCode opCode;
        private readonly uint CRC;
        private readonly uint payloadLength;
        private readonly object payloadData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="userID"></param>
        /// <param name="opCode"></param>
        /// <param name="CRC"></param>
        /// <param name="payloadLength"></param>
        /// <param name="payloadData"></param>
        public Packet(uint number, byte userID, OpCode opCode, uint CRC, uint payloadLength, object payloadData)
        {
            this.number = number;
            this.userID = userID;
            this.opCode = opCode;
            this.CRC = CRC;
            this.payloadLength = payloadLength;
            this.payloadData = payloadData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetNumber()
        {
            return number;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte GetUserID()
        {
            return userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OpCode GetOpCode()
        {
            return opCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetCRC()
        {
            return CRC;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetPayLoadLength()
        {
            return payloadLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetPayloadData()
        {
            return payloadData;
        }
    }
}
