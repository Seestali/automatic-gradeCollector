using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client.Network;
using client.utils;


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
        //TODO: Real documentation for function
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tbUser.Text + "\n\n" + Hash.GetHashString(tbPassword.Text));
            System.Diagnostics.Debug.WriteLine(Hash.GetHashString(tbPassword.Text));
            //TODO: send packets with credentials to server
            //TODO: get ACK/DEC back with data
            //TODO: open MainWindow with received information and show courses and grades
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
