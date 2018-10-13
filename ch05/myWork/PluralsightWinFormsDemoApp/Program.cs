using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            var culture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainFormView = new MainForm();
            var mainFormPresenter = new MainFormPresenter(mainFormView);
            Application.Run(mainFormView);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
                $"{((Exception)e.ExceptionObject).Message}\r\n" +
                "Please contact support.");
            Console.WriteLine($"ERROR {DateTimeOffset.Now}:{(Exception)e.ExceptionObject}");

            MessageBox.Show(message, "Unexpected Error");
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong.\r\n" +
                $"{e.Exception.Message}\r\n" +
                "Please contact support.");
            Console.WriteLine($"ERROR {DateTimeOffset.Now}:{e.Exception}");

            MessageBox.Show(message, "Unexpected Error");
        }
    }
}
