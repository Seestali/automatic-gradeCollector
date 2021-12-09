﻿using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace client.Utils
{
    public static class Hash
    {
        
        /// <summary>
        /// Function to convert user input to hashvalue in sha256 in uppercase letters
        /// </summary>
        /// <param name="inputString">Inputstring from textfield</param>
        /// <returns></returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte hashbyte in GetHash(inputString))
                //X2-Format: Formats string as two uppercase hexadecimal characters
                sb.Append(hashbyte.ToString("X2"));
            return sb.ToString();
        }
        /// <summary>
        /// Converts every byte to SHA256
        /// </summary>
        /// <param name="inputString">Gets input from GetHashString</param>
        /// <returns></returns>
        private static byte[] GetHash(string inputString) 
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}