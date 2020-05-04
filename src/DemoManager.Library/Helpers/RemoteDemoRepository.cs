using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DemoManager.Library.Objects;
using Newtonsoft.Json;

namespace DemoManager.Library.Helpers
{
    public class RemoteDemoRepository : IDemoRepository
    {
        private readonly string authKey;
        private readonly string baseUrl;

        public RemoteDemoRepository(string authKey)
        {
            this.authKey = authKey;
            baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        }

        public IEnumerable<IDemo> Demos
        {
            get
            {
                var client = new WebClient();
                client.Headers.Add("demo-manager-auth-key", authKey);

                var response = client.DownloadString($"{baseUrl}/ClientDemoList");

                if (response.Contains("AuthError"))
                    throw new Exception("AuthError");
                
                var results = JsonConvert.DeserializeObject<dynamic>(response);

                var demos = new List<IDemo>();
                foreach (var item in results) demos.Add(new RemoteDemo(item));

                return demos;
            }
        }

        public async Task<IDemo> Store(IDemo demo, Action<int> progressChanged)
        {
            var json = JsonConvert.SerializeObject(demo);

            using (var client = new WebClient())
            {
                client.Headers.Add("demo-manager-auth-key", authKey);
                client.Headers.Add("demo-manager-game-type", demo.Version.ToString());
                client.Headers.Add("Meta", json);
                client.Headers.Add("Content-Type", "binary/octet-stream");

                client.UploadProgressChanged += (sender, args) => { progressChanged?.Invoke(args.ProgressPercentage); };

                var result = await client.UploadFileTaskAsync($"{baseUrl}/ClientUploadDemo",
                    "POST", ((LocalDemo) demo).Path);

                var resultText = Encoding.ASCII.GetString(result);

                if (!string.IsNullOrEmpty(resultText))
                    throw new Exception(resultText);
            }

            return new RemoteDemo(JsonConvert.DeserializeObject<dynamic>(json));
        }
    }
}