using System;
using System.Windows.Forms;
using System.Collections.Generic;
using client.Utils;

namespace client.Forms
{   
    
    
    public partial class MainWindow : Form
    {
        List<SubjectAndGrade> subjectsAndGrades = new List<SubjectAndGrade>();
        //TODO: implement ign on google docdes
        //TODO: class for subjects + grades (subject id for list)
        //class used for showing disassembled packet and assembling set subjects /grades packet
        //TODO: implement finish / send function for UI (only when data has been changed)
        //TODO: dropdownmenu (value = value to build request) + request builder (corresponding button)
        
        public MainWindow()
        {
            InitializeComponent();
            
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 1, Name = "Messdaten", Grade = 1.2});
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 2, Name = "Informatik", Grade = 2.0});
            subjectsAndGrades.Add(new SubjectAndGrade() { Id = 3, Name = "Mathematik", Grade = 3.4});

            dgSubjectsGrades.DataSource = subjectsAndGrades;
        }
       
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Send packet to request subjects and grades
            //as int semester
            //Manager.GetInstance().SendGetSubjectsAndGradesRequest();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (SubjectAndGrade item in subjectsAndGrades)
                System.Diagnostics.Debug.WriteLine(item.Id + " " + item.Name + " " + item.Grade + "\n"); 
        }
    }
}
