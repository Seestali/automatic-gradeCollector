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
            this.btnDone = new System.Windows.Forms.Button();
            this.lbUser = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.dgSubjectsGrades = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize) (this.dgSubjectsGrades)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSemester
            // 
            this.cbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemester.Items.AddRange(new object[] {"Semester 1", "Semester 2", "Semester 3", "Semester 4", "Semester 5", "Semester 6"});
            this.cbSemester.Location = new System.Drawing.Point(13, 13);
            this.cbSemester.Name = "cbSemester";
            this.cbSemester.Size = new System.Drawing.Size(125, 28);
            this.cbSemester.TabIndex = 0;
            this.cbSemester.SelectedIndexChanged += new System.EventHandler(this.cbSemester_SelectedIndexChanged);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(611, 377);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 35);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Fertig";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
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
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(426, 12);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(260, 26);
            this.tbUser.TabIndex = 4;
            // 
            // dgSubjectsGrades
            // 
            this.dgSubjectsGrades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSubjectsGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubjectsGrades.Location = new System.Drawing.Point(12, 47);
            this.dgSubjectsGrades.Name = "dgSubjectsGrades";
            this.dgSubjectsGrades.RowTemplate.Height = 28;
            this.dgSubjectsGrades.Size = new System.Drawing.Size(674, 324);
            this.dgSubjectsGrades.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 424);
            this.Controls.Add(this.dgSubjectsGrades);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.lbUser);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.cbSemester);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize) (this.dgSubjectsGrades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgSubjectsGrades;

        private System.Windows.Forms.DataGridView dgSubjectAndGrades;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.TextBox tbUser;

        #endregion

        private System.Windows.Forms.ComboBox cbSemester;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label lbUser;
    }
}