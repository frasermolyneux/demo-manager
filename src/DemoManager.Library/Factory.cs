using DemoManager.Library.Helpers;

namespace DemoManager.Library
{
    public static class Factory
    {
        private static GameDetection gameDetection;

        public static GameDetection GameDetection => gameDetection ?? (gameDetection = new GameDetection());
    }
}