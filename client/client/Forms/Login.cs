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
            MessageBox.Show(tbUser.Text);
            MessageBox.Show(GetHashString(tbPassword.Text));
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
    }
}
