using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DemoManager.App.Configuration;
using DemoManager.App.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DemoManager.App.Repositories
{
    public class RemoteDemoRepository : IDemoRepository
    {
        public IEnumerable<IDemo> Demos
        {
            get
            {
                var client = new WebClient();
                client.Headers.Add("demo-manager-auth-key", DemoManagerConfiguration.AuthKey);

                var response = client.DownloadString($"{DemoManagerConfiguration.BaseUrl}/ClientDemoList");

                if (response.Contains("AuthError"))
                    throw new Exception(response);

                var results = JsonConvert.DeserializeObject<dynamic>(response);

                var demos = new List<IDemo>();
                foreach (var item in results) demos.Add(new RemoteDemo(item));

                return demos;
            }
        }

        public async Task<IDemo> Store(IDemo demo, Action<int> progressChanged)
        {
            var json = JsonConvert.SerializeObject(demo, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            using (var client = new WebClient())
            {
                client.Headers.Add("demo-manager-auth-key", DemoManagerConfiguration.AuthKey);
                client.Headers.Add("demo-manager-game-type", demo.Version.ToString());
                client.Headers.Add("Meta", json);
                client.Headers.Add("Content-Type", "binary/octet-stream");

                client.UploadProgressChanged += (sender, args) => { progressChanged?.Invoke(args.ProgressPercentage); };

                var result = await client.UploadFileTaskAsync($"{DemoManagerConfiguration.BaseUrl}/ClientUploadDemo",
                    "POST", ((LocalDemo) demo).Path);

                var resultText = Encoding.ASCII.GetString(result);

                if (!string.IsNullOrEmpty(resultText))
                    throw new Exception(resultText);
            }

            return new RemoteDemo(JsonConvert.DeserializeObject<dynamic>(json));
        }
    }
}