using System;
using System.Windows.Forms;

namespace client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Manager.GetInstance() instantiates the manager
            // GetForm() gets the first Form to display and pass it to Run()
            Application.Run(Manager.GetInstance().GetForm(Forms.CustomForms.Login));
        }
    }
}
