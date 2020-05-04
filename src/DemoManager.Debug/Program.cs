using System;
using System.Linq;
using DemoManager.Library;

namespace DemoManager.Debug
{
    // this program will be used to debug problems on a clients computer. 
    internal class Program
    {
        private static void Main()
        {
            // List installed games

            var games = Factory.GameDetection.GetInstalledGames();
            foreach (var game in games)
            {
                Console.WriteLine("Game Installed: {0}", game.Name);
                Console.WriteLine("    Path: {0}", game.InstallPath);
                Console.WriteLine("    Mods: {0}", game.Mods.Count());
                Console.WriteLine("    Demos: {0}", game.Demos.Count());
            }

            Console.ReadLine();
        }
    }
}