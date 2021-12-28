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
        /// <summary>
        /// Login button which sends credentials to configured server.
        /// If credentials are correct, MainWindow opens with information.
        /// If credentials are declined, corresponding error message appears.
        /// </summary>
        /// <param name="sender">sender is the login button on the login window</param>
        /// <param name="e">button click event</param>
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
                    Manager.getInstance().OpenForm<MainWindow>();
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
    }
}
