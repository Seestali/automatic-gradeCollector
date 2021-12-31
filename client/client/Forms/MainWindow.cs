using System;
using System.Windows.Forms;
using System.Collections.Generic;
using client.Utils;

namespace client.Forms
{   
    //TODO: Comments
    
    public partial class MainWindow : Form
    {
        List<SubjectAndGrade> subjectsAndGrades = new List<SubjectAndGrade>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataGridView();

        }

        private void InitializeDataGridView()
        {
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 1, Name = "Messdaten", Grade = 1.2});
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 2, Name = "Informatik", Grade = 2.0});
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 3, Name = "Mathematik", Grade = 3.4});

            dgSubjectsGrades.DataSource = subjectsAndGrades;
            dgSubjectsGrades.Columns[0].Visible = false;
            // dgSubjectsGrades.Columns[1].Width = 300;
            // dgSubjectsGrades.Columns[2].Width = dgSubjectsGrades.Width - dgSubjectsGrades.Columns[1].Width -50;
            dgSubjectsGrades.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Send packet to request subjects and grades
            //as int semester
            Manager.GetInstance().SendGetSubjectsAndGradesRequest(cbSemester.SelectedIndex + 1);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (SubjectAndGrade item in subjectsAndGrades)
                System.Diagnostics.Debug.WriteLine(item.Id + " " + item.Name + " " + item.Grade + "\n"); 
        }
    }
}
