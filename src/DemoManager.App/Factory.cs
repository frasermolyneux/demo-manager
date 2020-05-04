using DemoManager.App.Helpers;
using DemoManager.App.Repositories;

namespace DemoManager.App
{
    public static class Factory
    {
        private static GameDetection gameDetection;
        private static DemoPlayback demoPlayback;
        private static LocalDemoRepository localDemoRepository;
        private static ConfigEditor configEditor;
        private static RemoteDemoRepository remoteDemoRepository;

        public static GameDetection GameDetection => gameDetection ?? (gameDetection = new GameDetection());
        public static DemoPlayback DemoPlayback => demoPlayback ?? (demoPlayback = new DemoPlayback());

        public static LocalDemoRepository LocalDemoRepository =>
            localDemoRepository ?? (localDemoRepository = new LocalDemoRepository());

        public static RemoteDemoRepository RemoteDemoRepository =>
            remoteDemoRepository ?? (remoteDemoRepository = new RemoteDemoRepository());

        public static ConfigEditor ConfigEditor => configEditor ?? (configEditor = new ConfigEditor());
    }
}