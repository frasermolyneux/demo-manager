using System.Collections.Generic;
using DemoManager.App.Objects;
using Microsoft.Win32;

namespace DemoManager.App.Helpers
{
    public class GameDetection
    {
        private readonly Dictionary<GameVersion, string> games = new Dictionary<GameVersion, string>
        {
            {GameVersion.CallOfDuty2, "Call of Duty 2"},
            {GameVersion.CallOfDuty4, "Call of Duty 4"},
            {GameVersion.CallOfDuty5, "Call of Duty WAW"}
        };

        public GameDetection()
        {
            InstalledGames = new List<Game>();
            DetectInstalledGames();
        }

        private List<Game> InstalledGames { get; set; }

        public List<Game> GetInstalledGames()
        {
            return InstalledGames;
        }

        public void RefreshInstalledGames()
        {
            InstalledGames = new List<Game>();
            DetectInstalledGames();
        }

        private void DetectInstalledGames()
        {
            foreach (var pair in games)
            {
                var game = GameInstalled(pair.Key, pair.Value);

                if (game != null) InstalledGames.Add(game);
            }
        }

        private Game GameInstalled(GameVersion version, string name)
        {
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\" + name, "codkey", null) != null)
            {
                var game = new Game(version, name);
                var path = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Activision\" + name, "InstallPath", null);

                if (path != null) game.InstallPath = path.ToString();

                return game;
            }

            return null;
        }
    }
}