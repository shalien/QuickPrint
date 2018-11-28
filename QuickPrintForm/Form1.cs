using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace QuickPrintForm
{
    public partial class PrinterForm : Form
    {
        public PrinterForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void PrinterForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            iconTaskbar.Visible = true;
        }

        public void PrintFiles(ReadOnlyCollection<string> eCommandLine)
        {
            foreach (var str in eCommandLine)
            {
                if (!File.Exists(str)) return;

                if (str.EndsWith("exe")) continue;

                var info = new ProcessStartInfo(str.Trim())
                {
                    Verb = "Print", CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden
                };


                Process.Start(info);
            }
        }

        private void PrinterForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
            iconTaskbar.Visible = true;
        }
    }
}