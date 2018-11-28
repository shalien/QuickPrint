using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace QuickPrintForm
{
    internal static class Program
    {
        private static PrinterForm _mainForm;

        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainForm = new PrinterForm
            {
                WindowState = FormWindowState.Minimized
            };
            _mainForm.Hide();
            SingleInstanceApplication.Run(new PrinterForm(), NewInstanceHandler);
        }

        public static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            _mainForm.Hide();
            _mainForm.WindowState = FormWindowState.Minimized;
            _mainForm.PrintFiles(e.CommandLine);   
        }

        public class SingleInstanceApplication : WindowsFormsApplicationBase
        {
            private SingleInstanceApplication()
            {
                IsSingleInstance = true;
            }

            public static void Run(Form form, StartupNextInstanceEventHandler startupHandler)
            {
                form.Hide();
                form.WindowState = FormWindowState.Minimized;
                var app = new SingleInstanceApplication {MainForm = form};
                app.StartupNextInstance += startupHandler;
                app.Run(Environment.GetCommandLineArgs());
            }
        }
    }
}