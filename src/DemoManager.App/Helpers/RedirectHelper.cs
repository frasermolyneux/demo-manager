using System.IO;
using System.Net;
using System.Threading.Tasks;
using DemoManager.App.Configuration;

namespace DemoManager.App.Helpers
{
    /// <summary>
    ///     Contains helper methods for interacting with the XI redirect web server.
    /// </summary>
    public static class RedirectHelper
    {
        private static WebClient CreateClient()
        {
            var client = new WebClient();
            client.Headers.Add("demo-manager-auth-key", DemoManagerConfiguration.AuthKey);

            return client;
        }

        public static Stream DownloadFile(string address)
        {
            var client = CreateClient();
            return client.OpenRead(address);
        }

        public static Task DownloadFile(string address, string fileName)
        {
            var client = CreateClient();
            return client.DownloadFileTaskAsync(address, fileName);
        }

        public static Task DownloadFile(string address, string fileName,
            DownloadProgressChangedEventHandler progressChanged)
        {
            var client = CreateClient();
            client.DownloadProgressChanged += progressChanged;
            return client.DownloadFileTaskAsync(address, fileName);
        }
    }
}