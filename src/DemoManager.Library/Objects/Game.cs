using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DemoManager.Library.Helpers;

namespace DemoManager.Library.Objects
{
    public class Game
    {
        private readonly string _codWawAppDataPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Activision",
                "CoDWaW");

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="name">The name.</param>
        public Game(GameVersion version, string name)
        {
            InstallPath = string.Empty;
            Version = version;
            Name = name;
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Version.GetFullName();
        }

        #endregion

        #region Properties of Game

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets or sets the install path.
        /// </summary>
        public string InstallPath { get; set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     Gets the mods.
        /// </summary>
        public IEnumerable<Mod> Mods
        {
            get
            {
                switch (Version)
                {
                    case GameVersion.CallOfDuty4:
                        foreach (
                            var mod in
                            FoldersInPath(Path.Combine(InstallPath, "Mods"), (path, name) => new Mod(name, path))
                        )
                            yield return mod;

                        break;
                    case GameVersion.CallOfDuty5:
                        foreach (
                            var mod in
                            FoldersInPath(
                                Path.Combine(_codWawAppDataPath, "mods"), (path, name) => new Mod(name, path)))
                            yield return mod;

                        break;

                    case GameVersion.CallOfDuty2:
                        foreach (
                            var mod in
                            FoldersInPath(InstallPath,
                                (path, name) =>
                                    File.Exists(Path.Combine(path, "hunkusage.dat")) ? new Mod(name, path) : null))
                            yield return mod;
                        break;
                }
            }
        }

        /// <summary>
        ///     Gets the demos.
        /// </summary>
        public IEnumerable<LocalDemo> Demos
        {
            get
            {
                switch (Version)
                {
                    case GameVersion.CallOfDuty4:
                        // Read demos in main folder.
                        var mainDemosPath = Path.Combine(InstallPath, "main", "demos");

                        if (Directory.Exists(mainDemosPath))
                            foreach (var path in Directory.GetFiles(mainDemosPath, "*.dm_1"))
                                yield return new LocalDemo(path, Version);

                        // Read demos in mods folder.
                        foreach (var path in
                            Mods.Select(mod => Path.Combine(mod.Path, "demos"))
                                .Where(Directory.Exists)
                                .SelectMany(modDemosPath => Directory.GetFiles(modDemosPath, "*.dm_1")))
                            yield return new LocalDemo(path, Version);

                        break;
                    case GameVersion.CallOfDuty5:
                        // Read demos from user's local folder.
                        var demosPath = Path.Combine(_codWawAppDataPath, "players", "demos");

                        if (Directory.Exists(demosPath))
                            foreach (var path in Directory.GetFiles(demosPath, "*.dm_6"))
                                yield return new LocalDemo(path, Version);

                        break;
                    case GameVersion.CallOfDuty2:
                        // Read demos in mods folder.
                        foreach (var path in
                            Mods.Select(mod => Path.Combine(mod.Path, "demos"))
                                .Where(Directory.Exists)
                                .SelectMany(modDemosPath => Directory.GetFiles(modDemosPath, "*.dm_1")))
                            yield return new LocalDemo(path, Version);
                        break;
                }
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the game is running.
        /// </summary>
        public bool IsRunning => Process.GetProcessesByName(GetMultiplayerExecutableName()).Any();

        #endregion

        #region Private Methods of Game

        /// <summary>
        ///     Calls the specified <paramref name="func" /> for every folder in the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="func">The function.</param>
        /// <returns>The values returned by the specified func.</returns>
        private static IEnumerable<T> FoldersInPath<T>(string path, Func<string, string, T> func)
        {
            if (!Directory.Exists(path))
                yield break;

            foreach (
                var result in
                Directory.GetDirectories(path)
                    .Select(folder => func(folder, Path.GetFileName(folder)))
                    .Where(result => result != null))
                yield return result;
        }

        private string GetShortGameName()
        {
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                    return "cod2";
                case GameVersion.CallOfDuty4:
                    return "cod4";
                case GameVersion.CallOfDuty5:
                    return "cod5";
                default:
                    throw new Exception("Unsupported version");
            }
        }

        private IEnumerable<string> GetDataFolders()
        {
            yield return InstallPath;

            // Any zone.
            if (Version != GameVersion.CallOfDuty2)
                foreach (var path in Directory.GetDirectories(Path.Combine(InstallPath, "zone")))
                    yield return path;

            if (Version == GameVersion.CallOfDuty5)
                yield return _codWawAppDataPath;
        }

        private string GetDataDownloadFolder()
        {
            return Version == GameVersion.CallOfDuty5 ? _codWawAppDataPath : InstallPath;
        }

        private string GetMultiplayerExecutableName()
        {
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                    return "CoD2MP_s";
                case GameVersion.CallOfDuty4:
                    return "iw3mp";
                case GameVersion.CallOfDuty5:
                    return File.Exists(Path.Combine(InstallPath, "CodWaWmp_demo.exe")) ? "CoDWaWmp_demo" : "CoDWaWmp";
                default:
                    throw new Exception("Unsupported version");
            }
        }

        #endregion

        #region Public Methods of Game

        /// <summary>
        ///     Gets the path to the players folder.
        /// </summary>
        /// <param name="mod">The name of the active mod.</param>
        /// <returns>The path to the players folder.</returns>
        /// <exception cref="System.Exception">Unsupported version</exception>
        public string GetPlayersPath(string mod = null)
        {
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                    return Path.Combine(InstallPath, string.IsNullOrWhiteSpace(mod) ? "main" : mod, "players");
                case GameVersion.CallOfDuty4:
                    return Path.Combine(InstallPath, "players");
                case GameVersion.CallOfDuty5:
                    return Path.Combine(_codWawAppDataPath, "players");
                default:
                    throw new Exception("Unsupported version");
            }
        }

        /// <summary>
        ///     Gets the path to the profiles folder.
        /// </summary>
        /// <param name="mod">The name of the active mod.</param>
        /// <returns>The path to the profiles folder.</returns>
        /// <exception cref="System.Exception">Unsupported version</exception>
        public string GetProfilesPath(string mod = null)
        {
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                    return GetPlayersPath(mod);
                case GameVersion.CallOfDuty4:
                case GameVersion.CallOfDuty5:
                    return Path.Combine(GetPlayersPath(mod), "profiles");
                default:
                    throw new Exception("Unsupported version");
            }
        }

        /// <summary>
        ///     Gets the storage path for the specified demo.
        /// </summary>
        /// <param name="demo">The demo.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Thrown if demo is null.</exception>
        /// <exception cref="System.Exception">Unsupported version</exception>
        public string GetDemoStorageFolder(IDemo demo)
        {
            if (demo == null) throw new ArgumentNullException("demo");
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                    return Path.Combine(InstallPath, string.IsNullOrEmpty(demo.Mod) ? "main" : demo.Mod, "demos");
                case GameVersion.CallOfDuty4:
                    return string.IsNullOrEmpty(demo.Mod)
                        ? Path.Combine(InstallPath, "main", "demos")
                        : Path.Combine(InstallPath, "Mods", demo.Mod, "demos");
                case GameVersion.CallOfDuty5:
                    return Path.Combine(_codWawAppDataPath, "players", "demos");
                default:
                    throw new Exception("Unsupported version");
            }
        }

        /// <summary>
        ///     Gets a collection of missing files required for playing the specified <paramref name="demo" />.
        /// </summary>
        /// <param name="demo">The demo.</param>
        /// <returns>
        ///     A collection of tuples containing the URL where to download the missing file and the path where to store it
        ///     locally.
        /// </returns>
        /// <exception cref="System.Exception">Demo version differs from game</exception>
        public IEnumerable<Tuple<string, string>> GetMissingFiles(LocalDemo demo)
        {
            if (demo.Version != Version)
                throw new Exception("Demo version differs from game");

            // List IWD and FF files which cannot be found in any data folder. For each missing file return a tuple with
            // the download URL and the target path.
            return demo.IWDs.Select(iwd => string.Format("{0}.iwd", iwd))
                .Concat(demo.FFs.Select(ff => string.Format("{0}.ff", ff)))
                .Where(fileName => !GetDataFolders().Any(p => File.Exists(Path.Combine(p, fileName))))
                .Select(fileName => new Tuple<string, string>(
                    string.Format("http://redirect.xtremeidiots.net/redirect/{0}/{1}", GetShortGameName(), fileName),
                    Path.Combine(GetDataDownloadFolder(), fileName)));
        }

        /// <summary>
        ///     Gets the path to the active multiplayer configuration file.
        /// </summary>
        /// <param name="mod">The name of the active mod.</param>
        /// <returns>The path to the active multiplayer configuration file. Null if no configuration file is active.</returns>
        public string GetActiveMultiplayerConfigPath(string mod = null)
        {
            // Get the active profile name.
            var activePath = Path.Combine(GetProfilesPath(), "active.txt");

            if (!File.Exists(activePath))
                return null;

            var activeProfile = File.ReadAllText(activePath);

            // Locate the config file.
            var configPath = Path.Combine(GetProfilesPath(mod), activeProfile, "config_mp.cfg");

            return File.Exists(configPath) ? configPath : null;
        }

        /// <summary>
        ///     Gets the extension this game uses for demo files..
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Unsupported version</exception>
        public string GetDemoExtension()
        {
            switch (Version)
            {
                case GameVersion.CallOfDuty2:
                case GameVersion.CallOfDuty4:
                    return "dm_1";
                case GameVersion.CallOfDuty5:
                    return "dm_6";
                default:
                    throw new Exception("Unsupported version");
            }
        }

        /// <summary>
        ///     Gets the path to the multiplayer executable.
        /// </summary>
        /// <returns>The path to the multiplayer executable.</returns>
        /// <exception cref="System.Exception">Unsupported version</exception>
        public string GetMultiplayerExecutable()
        {
            return Path.Combine(InstallPath, string.Format("{0}.exe", GetMultiplayerExecutableName()));
        }

        #endregion
    }
}