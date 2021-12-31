using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace client.Forms
{
    //TODO: Comments
    public partial class Login : Form
    {
        private const string EMAIL_REGEX = "[a-z]+\\.[a-z]+@de.abb.com";
        private const string PASSWORD_REGEX = "[ -ÿ]+";
        
        private readonly Regex regexUser;
        private readonly Regex regexPassword;
        private bool userValid;
        private bool passwordValid;

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

        private void TbPassword_TextChanged(object sender, EventArgs e)
        {
            passwordValid = regexPassword.IsMatch(TbPassword.Text);
            Evaluate();
        }

        private void Evaluate()
        {
            BtnLogin.Enabled = userValid && passwordValid;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Manager.GetInstance().SendLoginRequest(TbUser.Text.Trim(), TbPassword.Text);
            BtnLogin.Enabled = false;
        }

        public void LoginVerified()
        {
            LbError.Invoke((MethodInvoker)delegate
            {
                LbError.Text = "Login successful";
                Hide();
                Form mainWindow = Manager.GetInstance().GetForm(Forms.CustomForms.MainWindow);
                mainWindow.FormClosed += (s, args) => Close();
                mainWindow.Show();
            });
        }

        public void SetErrorText(string error)
        {
            LbError.Invoke((MethodInvoker)delegate
            {
                LbError.Text = error;
            });
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

        private void BtnTest_Click(object sender, EventArgs e)
        {
            Manager.GetInstance().SendLoginRequest("test@test.de", "test");
            BtnLogin.Enabled = false;
        }
    }
}
