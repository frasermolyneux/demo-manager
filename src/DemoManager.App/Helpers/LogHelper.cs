using System;
using log4net;
using log4net.Config;

namespace DemoManager.App.Helpers
{
    public sealed class LogHelper
    {
        private static volatile LogHelper instance;
        private static readonly object SyncRoot = new object();
        private readonly ILog logger;

        private LogHelper()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger("DemoManager");
        }

        public static LogHelper Instance
        {
            get
            {
                if (instance != null) return instance;

                lock (SyncRoot)
                {
                    if (instance == null) instance = new LogHelper();
                }

                return instance;
            }
        }

        public void LogException(string message)
        {
            logger.Error(message);
        }

        public void LogException(string message, params object[] objects)
        {
            logger.Error(string.Format(message, objects));
        }

        public void LogException(Exception exception)
        {
            logger.Error("Exception", exception);
        }

        public void LogException(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }
    }
}