using System.Linq;
using System.Text.RegularExpressions;
using DemoManager.App.Objects;

namespace DemoManager.App.Helpers
{
    public static class GameHelper
    {
        public static Game GetGame(this GameVersion gameVersion)
        {
            return Factory.GameDetection.GetInstalledGames().FirstOrDefault(game => game.Version == gameVersion);
        }

        public static string StripColors(string input)
        {
            var regex = new Regex(@"\^[0-9]");

            return regex.Replace(input, "");
        }

        public static string GetFullName(this GameVersion gameVersion)
        {
            switch (gameVersion)
            {
                case GameVersion.CallOfDuty2:
                    return "Call of Duty 2";
                case GameVersion.CallOfDuty4:
                    return "Call of Duty 4: Modern Warfare";
                case GameVersion.CallOfDuty5:
                    return "Call of Duty: World at War";
                default:
                    return gameVersion.ToString();
            }
        }

        public static string GetFullGamemodeName(string input)
        {
            switch (input)
            {
                case "ctf":
                    return "Capture the Flag";
                case "dm":
                    return "Deathmatch";
                case "dom":
                    return "Domination";
                case "ftag":
                    return "Freeze Tag";
                case "koth":
                    return "Headquarters";
                case "sab":
                    return "Sabotage";
                case "sd":
                    return "Search and Destroy";
                case "tdm":
                    return "Team Deathmatch";
                case "twar":
                    return "War";
                case "war":
                    return "Team Deathmatch";
                default:
                    return input;
            }
        }
    }
}