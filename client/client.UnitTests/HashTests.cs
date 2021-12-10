using System;
using client;
using client.Utils;
using Xunit;


namespace client.UnitTests
{
    public class HashTests
    {
        //TODO: add test cases for wrong input type
        //TODO: add test cases for no input
        //TODO: empty string
        [Fact]
        public void GetHashString_StringInput_ReturnsSame()
        {
            Assert.Equal("643F6BA68C9333859694078A905B90C4F036D01CF7E81D6EF0A6CA79A344B6B7",Hash.GetHashString("HalloHenny"));
        }
        
        [Fact]
        public void GetHashString_DifferentTypeInput_DenyInput()
        {

        private string a = "hello";
            Assert.Equal(1223,Hash.GetHashString("HalloHenny"));
        }
    }
}