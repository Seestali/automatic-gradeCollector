using System;
using System.Collections.Generic;
using System.Security;
using client.Utils;
using System.Windows.Forms;
using client.Network;

namespace client.Forms
{   
    
    
    public partial class MainWindow : Form
    {
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

        }
        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Send packet to request subjects and grades
            
        }

    }
}
