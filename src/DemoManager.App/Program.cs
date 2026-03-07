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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                TelemetryHelper.Instance.TrackException(args.ExceptionObject as Exception);
                TelemetryHelper.Instance.Flush();
            };

            Application.ThreadException += (sender, args) =>
            {
                TelemetryHelper.Instance.TrackException(args.Exception);
                TelemetryHelper.Instance.Flush();
            };

            TelemetryHelper.Instance.TrackEvent("ApplicationStart");

            Application.Run(new FrmMain());

            TelemetryHelper.Instance.TrackEvent("ApplicationExit");
            TelemetryHelper.Instance.Flush();
        }
    }
}