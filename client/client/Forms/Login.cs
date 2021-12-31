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
            regexUser = new Regex(EMAIL_REGEX);
            regexPassword = new Regex(PASSWORD_REGEX);
        }

        private void TbUser_TextChanged(object sender, EventArgs e)
        {
            userValid = regexUser.IsMatch(TbUser.Text.Trim());
            Evaluate();
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
            Manager.GetInstance().SendLoginRequest(TbUser.Text.Trim(), TbPassword.Text);
            BtnLogin.Enabled = false;
        }

        public void LoginVerified()
        {
            LbError.Text = "Login successful";
            Console.WriteLine("Login successful");
            Hide();
            Form mainWindow = Manager.GetInstance().GetForm(Forms.CustomForms.MainWindow);
            mainWindow.FormClosed += (s, args) => Close();
            mainWindow.Show();
        }

        public void SetErrorText(string error)
        {
            LbError.Text = error;
        }

        private void TbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                BtnLogin.PerformClick();
            }
        }

        private void TbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                BtnLogin.PerformClick();
            }
        }

        private void BtnSkip_Click(object sender, EventArgs e)
        {
            LoginVerified();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            Manager.GetInstance().SendLoginRequest("henrik.kaltenbach@de.abb.com", "password");
            BtnLogin.Enabled = false;
        }
    }
}
