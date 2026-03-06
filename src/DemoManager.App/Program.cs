using System;
using System.Windows.Forms;
using DemoManager.App.Helpers;

namespace DemoManager.App
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            TelemetryHelper.Instance.TrackEvent("ApplicationStart");

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                TelemetryHelper.Instance.TrackException(args.ExceptionObject as Exception);
                TelemetryHelper.Instance.Flush();
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());

            TelemetryHelper.Instance.TrackEvent("ApplicationExit");
            TelemetryHelper.Instance.Flush();
        }
    }
}