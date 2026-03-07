using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace DemoManager.App.Helpers
{
    public sealed class TelemetryHelper
    {
        private static volatile TelemetryHelper instance;
        private static readonly object SyncRoot = new object();

        private readonly TelemetryClient client;

        private TelemetryHelper()
        {
            try
            {
                var connectionString = ConfigurationManager.AppSettings["ApplicationInsightsConnectionString"];

                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    var config = TelemetryConfiguration.CreateDefault();
                    config.ConnectionString = connectionString;
                    client = new TelemetryClient(config);
                }
                else
                {
                    client = new TelemetryClient(TelemetryConfiguration.CreateDefault());
                }

                client.Context.Session.Id = Guid.NewGuid().ToString();
                client.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
                client.Context.Device.Id = Environment.MachineName;
                client.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
                client.Context.GlobalProperties["MachineName"] = Environment.MachineName;
            }
            catch
            {
                client = new TelemetryClient(TelemetryConfiguration.CreateDefault());
            }
        }

        public static TelemetryHelper Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (SyncRoot)
                {
                    if (instance == null) instance = new TelemetryHelper();
                }
                return instance;
            }
        }

        public void SetUser(string displayName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    client.Context.User.AuthenticatedUserId = displayName;
                    client.Context.GlobalProperties["DisplayName"] = displayName;
                }
            }
            catch
            {
                // Telemetry should never crash the application
            }
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            try
            {
                client.TrackEvent(eventName, properties, metrics);
            }
            catch
            {
                // Telemetry should never crash the application
            }
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null)
        {
            try
            {
                client.TrackException(exception, properties);
            }
            catch
            {
                // Telemetry should never crash the application
            }
        }

        public void Flush()
        {
            try
            {
                client.Flush();
            }
            catch
            {
                // Telemetry should never crash the application
            }
        }
    }
}
