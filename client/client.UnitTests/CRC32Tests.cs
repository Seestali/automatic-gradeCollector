﻿using System;
using System.Threading;
using client.Network;
using Xunit;
using Xunit.Sdk;

namespace client.UnitTests
{
    public class CRC32Tests
    {
        [Fact]
        public void CalculateChecksum_Hex1234567890_CheckSumIsSame()
        {
            byte[] array = {0x12, 0x34, 0x56, 0x78, 0x90};
            Assert.Equal(3700649649, CRC32.CalculateChecksum(ref array));
        }
        
        [Fact]
        public void CalculateChecksum_Hex1234567890_CheckFail()
        {
            byte[] array = {0x12, 0x34, 0x56, 0x78, 0x90};
            Assert.NotEqual(3700649648, CRC32.CalculateChecksum(ref array));
        }
        
        [Fact]
        public void CalculateChecksum_NullInput_ThrowsException()
        {
            byte[] array = null;
            Assert.Throws<NullReferenceException>(() => CRC32.CalculateChecksum( ref array));
        }
    }
}