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
            dgSubjectsGrades.DataSource = subjectsAndGrades;
            dgSubjectsGrades.Columns[0].Visible = false;
            dgSubjectsGrades.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            //disable datasource
            dgSubjectsGrades.DataSource = null;
            
            //send packet
            Manager.GetInstance().SendGetSubjectsAndGradesRequest(cbSemester.SelectedIndex + 1);
            
            //connect to datasource
            dgSubjectsGrades.DataSource = subjectsAndGrades;
            
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Manager.GetInstance().SendSetGradesRequest();
            foreach (SubjectAndGrade item in subjectsAndGrades)
                System.Diagnostics.Debug.WriteLine(item.Id + " " + item.Name + " " + item.Grade + "\n"); 
        }
    }
}
