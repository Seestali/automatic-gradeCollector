namespace client.Forms
{
    partial class MainWindow
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
            this.cbSemester = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.lbUser = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbSemester
            // 
            this.cbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemester.Items.AddRange(new string[] {
            "Semester 1",
            "Semester 2",
            "Semester 3",
            "Semester 4",
            "Semester 5",
            "Semester 6"});
            this.cbSemester.Location = new System.Drawing.Point(13, 13);
            this.cbSemester.Name = "cbSemester";
            this.cbSemester.Size = new System.Drawing.Size(125, 28);
            this.cbSemester.TabIndex = 0;
            this.cbSemester.SelectedIndexChanged += new System.EventHandler(this.cbSemester_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(13, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(673, 304);
            this.listBox1.TabIndex = 1;
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(611, 377);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 35);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Fertig";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Location = new System.Drawing.Point(346, 15);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(74, 20);
            this.lbUser.TabIndex = 3;
            this.lbUser.Text = "Benutzer";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(426, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(260, 26);
            this.textBox1.TabIndex = 4;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 424);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cbSemester);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSemester;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.TextBox textBox1;
    }
}