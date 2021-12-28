using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client.Forms;
using client.Network;
using client.Utils;


namespace client
{
    //TODO: remove debug login button
    //TODO: implement correct login function with packet assembly and udp send
    //TODO: disassemble received package and continue to build MainWindow with received data
    //TODO: deny message for failed auth.
    //TODO: function in manager class
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
        //TODO: Real documentation for function
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInputIsValid(tbUser.Text) && UserInputIsValid(tbPassword.Text))
                {
                    System.Diagnostics.Debug.WriteLine(Hash.GetHashString(tbPassword.Text));
                    //TODO: send packets with credentials to server
                    //TODO: get ACK/DEC back with data
                    //TODO: open MainWindow with received information and show courses and grades
                    Manager.GetInstance().OpenForm<MainWindow>();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect user input.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Check user input for wrong input.
        /// </summary>
        /// <param name="text">Text from inputfields for Mail and Password</param>
        /// <returns>Returns false if string is empty. True for valid input. </returns>
        private bool UserInputIsValid(string text)
        {
            return !string.IsNullOrEmpty(text);
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
