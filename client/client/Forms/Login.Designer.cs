﻿
namespace client
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
            this.credentialsBox = new System.Windows.Forms.GroupBox();
            this.btnSendReceive = new System.Windows.Forms.Button();
            this.lbUser = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.credentialsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // credentialsBox
            // 
            this.credentialsBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.credentialsBox.Controls.Add(this.btnSendReceive);
            this.credentialsBox.Controls.Add(this.lbUser);
            this.credentialsBox.Controls.Add(this.btnLogin);
            this.credentialsBox.Controls.Add(this.tbUser);
            this.credentialsBox.Controls.Add(this.lbPassword);
            this.credentialsBox.Controls.Add(this.tbPassword);
            this.credentialsBox.Location = new System.Drawing.Point(12, 12);
            this.credentialsBox.Name = "credentialsBox";
            this.credentialsBox.Size = new System.Drawing.Size(173, 125);
            this.credentialsBox.TabIndex = 5;
            this.credentialsBox.TabStop = false;
            // 
            // btnSendReceive
            // 
            this.btnSendReceive.Location = new System.Drawing.Point(10, 96);
            this.btnSendReceive.Name = "btnSendReceive";
            this.btnSendReceive.Size = new System.Drawing.Size(75, 23);
            this.btnSendReceive.TabIndex = 5;
            this.btnSendReceive.Text = "Send/Rec";
            this.btnSendReceive.UseVisualStyleBackColor = true;
            this.btnSendReceive.Visible = false;
            this.btnSendReceive.Click += new System.EventHandler(this.btnDebugSendReceive);
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Location = new System.Drawing.Point(24, 37);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(36, 13);
            this.lbUser.TabIndex = 4;
            this.lbUser.Text = "E-Mail";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(91, 96);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(66, 34);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 0;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(10, 63);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(50, 13);
            this.lbPassword.TabIndex = 3;
            this.lbPassword.Text = "Passwort";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(66, 60);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 150);
            this.Controls.Add(this.credentialsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(207, 179);
            this.MinimumSize = new System.Drawing.Size(207, 179);
            this.Name = "Login";
            this.Text = "Login";
            this.credentialsBox.ResumeLayout(false);
            this.credentialsBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnSendReceive;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.GroupBox credentialsBox;

        private System.Windows.Forms.GroupBox groupBox1;

        #endregion

        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
    }
}

