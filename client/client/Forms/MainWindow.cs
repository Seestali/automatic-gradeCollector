using System;
using System.Collections.Generic;
using System.Security;
using client.Utils;
using System.Windows.Forms;

namespace client.Forms
{   
    
    
    public partial class MainWindow : Form
    {
        private Tuple<int, string>[] semester =
        {
            new Tuple<int, string>(1, "Semester 1"),
            new Tuple<int, string>(2, "Semester 2"),
            new Tuple<int, string>(3, "Semester 3"),
            new Tuple<int, string>(4, "Semester 4"),
            new Tuple<int, string>(5, "Semester 5"),
            new Tuple<int, string>(6, "Semester 6")
        };
        //TODO: implement ign on google docdes
        //TODO: class for subjects + grades (subject id for list)
        //class used for showing disassembled packet and assembling set subjects /grades packet
        //TODO: implement finish / send function for UI (only when data has been changed)
        //TODO: dropdownmenu (value = value to build request) + request builder (corresponding button)
        
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void MainWindow_Load(object sender, EventArgs e)
        {
            populateSemesterCBox();
        }
        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Send packet to request subjects and grades
            MessageBox.Show(cbSemester.SelectedValue.ToString());
        }
        
        //Initialize combobox with content
        private void populateSemesterCBox()
        {
            cbSemester.DataSource = semester;
            cbSemester.DisplayMember = "Item2";
            cbSemester.ValueMember = "Item1";
        }
    }
}
