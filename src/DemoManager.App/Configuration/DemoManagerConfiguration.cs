using System.Configuration;
using DemoManager.App.Properties;

namespace DemoManager.App.Configuration
{
    public static class DemoManagerConfiguration
    {
        public static string BaseUrl => ConfigurationManager.AppSettings["BaseUrl"];

        public static string AuthKey
        {
            get
            {
                Settings.Default.Reload();
                return Settings.Default.AuthKey;
            }
        }
    }
}