using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DemoManager.Library.Objects;
using DemoManager.Library.Properties;

namespace DemoManager.Library.Helpers
{
    // this class will allow the modification of configs and will be used to ensure that admins have the correct binds
    // for recording/playback

    public class ConfigEditor
    {
        // When starting a playback (using BindPlaybackKeys) this dictionary will be filled with keys which have been
        // overwritten when binding the playback keys. UnbindPlaybackKeys rebinds these keys after the playback keys
        // have been unbound.
        private readonly Dictionary<Game, IEnumerable<Bind>> overwrittenBinds =
            new Dictionary<Game, IEnumerable<Bind>>();

        // Keys which are bound when starting a playback and unbound directly after.
        private readonly Bind[] playbackBinds =
        {
            new Bind("toggle timescale 0.1 0.25 0.5 1 2 5 10 15 15", "Increase Speed"),
            new Bind("toggle timescale 15 10 5 2 1 0.5 0.25 0.1 0.1", "Decrease Speed"),
            new Bind("set timescale 1", "Reset Speed"),
            new Bind("toggle cl_freezedemo; set timescale 1", "Pause/unpause"),
            new Bind("toggle g_compassShowEnemies", "Show/hide enemies on compass"),
            new Bind("toggle cg_draw2D", "Show/hide HUD"),
            new Bind("quit", "Quit"),
            new Bind("toggle cg_thirdPerson", "Enable/disable third person view"),
            new Bind("toggle cg_drawThroughWalls", "Enable/disable draw friendly nametags trough walls")
        };

        // Keys which will be bound at all time to start/stop recording demos.
        private readonly Bind[] recordingBinds =
        {
            new Bind("record", "Start recording"),
            new Bind("stoprecord", "Stop recording")
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigEditor" /> class.
        /// </summary>
        public ConfigEditor()
        {
            LoadBinds();
        }

        /// <summary>
        ///     Gets the playback keys as configured.
        /// </summary>
        public IEnumerable<Bind> PlaybackBinds => playbackBinds;

        /// <summary>
        ///     Gets the recording keys as configured.
        /// </summary>
        public IEnumerable<Bind> RecordingBinds => recordingBinds;

        /// <summary>
        ///     Splits the configuration into 3 lists, preBinds with lines before the "bind" lines in the cod configuration, binds
        ///     with the "bind" lines in the cod configuration and postBinds with lines after the "bind" lines in the cod
        ///     configuration.
        /// </summary>
        /// <param name="lines">An array of lines of the cod configuration.</param>
        /// <param name="preBinds">The list that should be populated with the lines before the "bind" lines.</param>
        /// <param name="binds">The list that should be populated with the "bind" lines.</param>
        /// <param name="postBinds">The list that should be populated with the lines after the "bind" lines.</param>
        private void SplitConfig(string[] lines, List<string> preBinds, List<Bind> binds, List<string> postBinds)
        {
            if (preBinds == null) throw new ArgumentNullException(nameof(preBinds));
            if (binds == null) throw new ArgumentNullException(nameof(binds));
            if (postBinds == null) throw new ArgumentNullException(nameof(postBinds));

            // Holds 0 when no "bind" lines have been found, 1 when the "bind" lines have been found, 2 when the lines
            // after the "bind" lines have been found.
            var stage = 0;

            foreach (var line in lines)
            {
                // Update the stage number.
                var isLineBind = line.StartsWith("bind");
                if (stage == 0 && isLineBind)
                    stage = 1;
                else if (stage == 1 && !isLineBind)
                    stage = 2;

                // Based on the stage number, add the line to one of the lists.
                switch (stage)
                {
                    case 0:
                        preBinds.Add(line);
                        break;
                    case 1:
                        var parts = line.Split(' ');
                        // format: bind/bind2 key "some command here"
                        binds.Add(new Bind(parts[0], parts[1],
                            line.Substring(parts[0].Length + 1 + parts[1].Length + 1).Trim('"'), ""));
                        break;
                    case 2:
                        postBinds.Add(line);
                        break;
                }
            }
        }

        /// <summary>
        ///     Joins the configuration lists back to an array of lines.
        /// </summary>
        /// <param name="preBinds">The lines before the "bind" lines.</param>
        /// <param name="binds">The "bind" lines.</param>
        /// <param name="postBinds">The lines after the "bind" lines.</param>
        /// <returns>Array of lines of the configuration.</returns>
        private string[] JoinConfig(List<string> preBinds, List<Bind> binds, List<string> postBinds)
        {
            return
                preBinds.Concat(binds.Select(bind => $"{bind.Type} {bind.Key} \"{bind.Value}\""))
                    .Concat(postBinds)
                    .ToArray();
        }

        /// <summary>
        ///     Opens the config file of the specified <paramref name="game" />, calls the specified action and then write the
        ///     modified configuration back to the file.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <param name="action">The action to perform once the file has been opened.</param>
        /// <returns>True on success; False otherwise.</returns>
        private bool RewriteBinds(Game game, Action<List<Bind>> action, string mod = null)
        {
            var path = game.GetActiveMultiplayerConfigPath(mod);
            if (path == null) return false;

            // Read config.
            var lines = File.ReadAllLines(path);

            var preBinds = new List<string>();
            var binds = new List<Bind>();
            var postBinds = new List<string>();

            SplitConfig(lines, preBinds, binds, postBinds);

            // Modify binds.
            action(binds);

            // Write config.
            File.WriteAllText(path, string.Join("\n", JoinConfig(preBinds, binds, postBinds)));

            return true;
        }

        /// <summary>
        ///     Binds the specified <paramref name="keys" /> in the configuration file of the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="overwritten">The key bindings which have been overwritten.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool BindKeys(Game game, IEnumerable<Bind> keys, out IEnumerable<Bind> overwritten, string mod = null)
        {
            overwritten = null;

            IEnumerable<Bind> overwrittenBindsList = null;

            if (!RewriteBinds(game, binds =>
            {
                // Find binds to be overwritten.
                overwrittenBindsList =
                    binds.Where(
                            bind => keys.Where(b => !string.IsNullOrWhiteSpace(b.Key)).Any(b => b.Key == bind.Key))
                        .ToArray();

                binds.RemoveAll(overwrittenBindsList.Contains);

                // Add all binds.
                binds.AddRange(keys.Where(b => !string.IsNullOrWhiteSpace(b.Key)));
            }, mod)) return false;

            overwritten = overwrittenBindsList;
            return true;
        }

        /// <summary>
        ///     Binds the specified <paramref name="keys" /> in the configuration file of the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <param name="keys">The keys.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool BindKeys(Game game, IEnumerable<Bind> keys, string mod = null)
        {
            return BindKeys(game, keys, out _, mod);
        }

        /// <summary>
        ///     Binds the specified <paramref name="keys" /> in the configuration file of the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="keys">The keys.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool BindKeysInAllMods(Game game, IEnumerable<Bind> keys)
        {
            if (game.Version != GameVersion.CallOfDuty2) return BindKeys(game, keys);
            foreach (var mod in game.Mods.ToList())
                BindKeys(game, keys, mod.Name);

            return BindKeys(game, keys);
        }

        /// <summary>
        ///     Unbinds the specified <paramref name="keys" /> in the configuration file of the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <param name="keys">The keys.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool UnbindKeys(Game game, IEnumerable<Bind> keys, string mod = null)
        {
            return RewriteBinds(game, binds =>
            {
                // Remove all binds.
                binds.RemoveAll(bind => keys.Any(b => bind.Value == b.Value));
            }, mod);
        }

        /// <summary>
        ///     Saves the key binds to the settings of this application.
        /// </summary>
        public void SaveBinds()
        {
            Settings.Default.PlaybackKeys = string.Join(";", PlaybackBinds.Select(b => b.Key));
            Settings.Default.RecordingKeys = string.Join(";", RecordingBinds.Select(b => b.Key));
            Settings.Default.Save();
        }

        /// <summary>
        ///     Loads the key binds from the settings of this application.
        /// </summary>
        private void LoadBinds()
        {
            if (Settings.Default.Migrate)
            {
                Settings.Default.Upgrade();
                Settings.Default.Migrate = false;
                Settings.Default.Save();
            }

            var i = 0;
            foreach (var key in Settings.Default.PlaybackKeys.Split(';').TakeWhile(key => i < playbackBinds.Length))
                playbackBinds[i++].Key = string.IsNullOrWhiteSpace(key) ? null : key;

            i = 0;
            foreach (var key in Settings.Default.RecordingKeys.Split(';').TakeWhile(key => i < recordingBinds.Length))
                recordingBinds[i++].Key = string.IsNullOrWhiteSpace(key) ? null : key;
        }

        /// <summary>
        ///     Binds the playback keys for the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool BindPlaybackKeys(Game game, string mod = null)
        {
            IEnumerable<Bind> overwritten;
            if (!BindKeys(game, PlaybackBinds, out overwritten, mod)) return false;

            if (overwritten.Any())
                overwrittenBinds[game] = overwritten;

            return true;
        }

        /// <summary>
        ///     Unbinds the playback keys for the specified <paramref name="game" />.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mod">The name of the active mod.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool UnbindPlaybackKeys(Game game, string mod = null)
        {
            if (!UnbindKeys(game, PlaybackBinds, mod)) return false;

            if (overwrittenBinds.ContainsKey(game))
            {
                BindKeys(game, overwrittenBinds[game], mod);

                overwrittenBinds.Remove(game);
            }

            return true;
        }
    }
}