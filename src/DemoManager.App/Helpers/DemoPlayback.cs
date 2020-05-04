using System;
using System.Diagnostics;
using DemoManager.App.Objects;

namespace DemoManager.App.Helpers
{
    // this class will control the demo playback for demos that are in the local repository

    public class DemoPlayback
    {
        public Process Start(LocalDemo localDemo)
        {
            if (localDemo == null) throw new ArgumentNullException(nameof(localDemo));

            var game = localDemo.Version.GetGame();

            var info = new ProcessStartInfo(game.GetMultiplayerExecutable(), GetArgumentsForDemo(localDemo))
            {
                WorkingDirectory = game.InstallPath
            };

            return Process.Start(info);
        }

        private string GetArgumentsForDemo(LocalDemo localDemo)
        {
            if (localDemo.Mod == null)
                return string.Format("+seta thereisacow 1337 +seta sv_cheats 1 +devmap {1} +killserver +demo \"{0}\"",
                    localDemo.Name, localDemo.Map);

            return
                string.Format(
                    "+seta thereisacow 1337 +seta sv_cheats 1 +set fs_game {3}{0} +devmap {2} +killserver +demo \"{1}\"",
                    localDemo.Mod, localDemo.Name, localDemo.Map,
                    localDemo.Version == GameVersion.CallOfDuty2 ? string.Empty : "mods/");
        }
    }
}