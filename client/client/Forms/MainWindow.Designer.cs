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
            this.CbSemester = new System.Windows.Forms.ComboBox();
            this.LbUser = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.LbxSubjectsAndGrades = new System.Windows.Forms.ListBox();
            this.BtnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CbSemester
            // 
            this.CbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbSemester.Items.AddRange(new object[] {
            "Semester 1",
            "Semester 2",
            "Semester 3",
            "Semester 4",
            "Semester 5",
            "Semester 6"});
            this.CbSemester.Location = new System.Drawing.Point(13, 13);
            this.CbSemester.Name = "CbSemester";
            this.CbSemester.Size = new System.Drawing.Size(125, 28);
            this.CbSemester.TabIndex = 0;
            this.CbSemester.SelectedIndexChanged += new System.EventHandler(this.CbSemester_SelectedIndexChanged);
            // 
            // LbUser
            // 
            this.LbUser.AutoSize = true;
            this.LbUser.Location = new System.Drawing.Point(346, 15);
            this.LbUser.Name = "LbUser";
            this.LbUser.Size = new System.Drawing.Size(74, 20);
            this.LbUser.TabIndex = 3;
            this.LbUser.Text = "Benutzer";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(426, 12);
            this.tbUser.Name = "tbUser";
            this.tbUser.ReadOnly = true;
            this.tbUser.Size = new System.Drawing.Size(260, 26);
            this.tbUser.TabIndex = 4;
            // 
            // LbxSubjectsAndGrades
            // 
            this.LbxSubjectsAndGrades.FormattingEnabled = true;
            this.LbxSubjectsAndGrades.ItemHeight = 20;
            this.LbxSubjectsAndGrades.Location = new System.Drawing.Point(13, 48);
            this.LbxSubjectsAndGrades.Name = "LbxSubjectsAndGrades";
            this.LbxSubjectsAndGrades.Size = new System.Drawing.Size(673, 304);
            this.LbxSubjectsAndGrades.TabIndex = 1;
            // 
            // BtnDone
            // 
            this.BtnDone.Location = new System.Drawing.Point(611, 377);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(75, 35);
            this.BtnDone.TabIndex = 2;
            this.BtnDone.Text = "Fertig";
            this.BtnDone.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 424);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.LbUser);
            this.Controls.Add(this.BtnDone);
            this.Controls.Add(this.LbxSubjectsAndGrades);
            this.Controls.Add(this.CbSemester);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CbSemester;
        private System.Windows.Forms.ListBox LbxSubjectsAndGrades;
        private System.Windows.Forms.Button BtnDone;
        private System.Windows.Forms.Label LbUser;
        private System.Windows.Forms.TextBox tbUser;
    }
}
