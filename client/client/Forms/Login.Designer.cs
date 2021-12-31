namespace client.Forms
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LbUser = new System.Windows.Forms.Label();
            this.TbUser = new System.Windows.Forms.TextBox();
            this.LbPassword = new System.Windows.Forms.Label();
            this.TbPassword = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.LbError = new System.Windows.Forms.Label();
            this.BtnSkip = new System.Windows.Forms.Button();
            this.BtnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LbUser
            // 
            this.LbUser.AutoSize = true;
            this.LbUser.Location = new System.Drawing.Point(12, 15);
            this.LbUser.Name = "LbUser";
            this.LbUser.Size = new System.Drawing.Size(53, 20);
            this.LbUser.TabIndex = 0;
            this.LbUser.Text = "E-Mail";
            // 
            // TbUser
            // 
            this.TbUser.Location = new System.Drawing.Point(96, 12);
            this.TbUser.Name = "TbUser";
            this.TbUser.Size = new System.Drawing.Size(260, 26);
            this.TbUser.TabIndex = 1;
            this.TbUser.TextChanged += new System.EventHandler(this.TbUser_TextChanged);
            this.TbUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbUser_KeyPress);
            // 
            // LbPassword
            // 
            this.LbPassword.AutoSize = true;
            this.LbPassword.Location = new System.Drawing.Point(12, 48);
            this.LbPassword.Name = "LbPassword";
            this.LbPassword.Size = new System.Drawing.Size(78, 20);
            this.LbPassword.TabIndex = 2;
            this.LbPassword.Text = "Password";
            // 
            // TbPassword
            // 
            this.TbPassword.Location = new System.Drawing.Point(96, 45);
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.Size = new System.Drawing.Size(260, 26);
            this.TbPassword.TabIndex = 3;
            this.TbPassword.UseSystemPasswordChar = true;
            this.TbPassword.TextChanged += new System.EventHandler(this.TbPassword_TextChanged);
            this.TbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbPassword_KeyPress);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Enabled = false;
            this.BtnLogin.Location = new System.Drawing.Point(285, 80);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(75, 35);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // LbError
            // 
            this.LbError.AutoSize = true;
            this.LbError.Location = new System.Drawing.Point(13, 112);
            this.LbError.Name = "LbError";
            this.LbError.Size = new System.Drawing.Size(0, 20);
            this.LbError.TabIndex = 5;
            // 
            // BtnSkip
            // 
            this.BtnSkip.Location = new System.Drawing.Point(96, 80);
            this.BtnSkip.Name = "BtnSkip";
            this.BtnSkip.Size = new System.Drawing.Size(75, 35);
            this.BtnSkip.TabIndex = 6;
            this.BtnSkip.Text = "skip";
            this.BtnSkip.UseVisualStyleBackColor = true;
            this.BtnSkip.Click += new System.EventHandler(this.BtnSkip_Click);
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(177, 80);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(75, 35);
            this.BtnTest.TabIndex = 7;
            this.BtnTest.Text = "test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 144);
            this.Controls.Add(this.BtnTest);
            this.Controls.Add(this.BtnSkip);
            this.Controls.Add(this.LbError);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TbPassword);
            this.Controls.Add(this.LbPassword);
            this.Controls.Add(this.TbUser);
            this.Controls.Add(this.LbUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbUser;
        private System.Windows.Forms.TextBox TbUser;
        private System.Windows.Forms.Label LbPassword;
        private System.Windows.Forms.TextBox TbPassword;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Label LbError;
        private System.Windows.Forms.Button BtnSkip;
        private System.Windows.Forms.Button BtnTest;
    }
}