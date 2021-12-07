using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using client.Network;

namespace client
{
    public partial class Login : Form
    {
        /// <summary>
        /// First shown window to user.
        /// Grades only accessible after successful login.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //TODO: make login and proceed with program (close Login-Form and create new)
            MessageBox.Show(tbUser.Text + "\n\n" + GetHashString(tbPassword.Text));
        }

        //TODO: Create separate class for hashing function
        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte hashbyte in GetHash(inputString))
                //X2-Format: Formats string as two uppercase hexadecimal characters
                sb.Append(hashbyte.ToString("X2"));
            return sb.ToString();
        }

        private void btnDebugSendReceive(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Started Sever\n");
            // UDP2 s = new UDP2();
            //s.Server("127.0.0.1", 42069);
            
            UdpProtocol c = new UdpProtocol();
            c.Client("178.203.36.119", 42069);
            c.Send("Hallo Henny");
            System.Diagnostics.Debug.WriteLine("Sent message");
            //When using server: when you want to start the server again, server needs to be closed before.
            //or closing bug (System.ObjectDisposedException)
            //s._socket.Close() //closes socked for outgoing and incoming udp packets
        }
    }
}
