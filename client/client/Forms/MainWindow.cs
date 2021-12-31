using System;
using System.Windows.Forms;

namespace client.Forms
{
    public partial class MainWindow : Form
    {
        //TODO: Rename form
        //TODO: implement ign on google docdes
        //TODO: class for subjects + grades (subject id for list)
        //class used for showing disassembled packet and assembling set subjects /grades packet
        //TODO: implement finish / send function for UI (only when data has been changed)
        //TODO: dropdownmenu (value = value to build request) + request builder (corresponding button)
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Send packet to request subjects and grades
        }
    }
}
