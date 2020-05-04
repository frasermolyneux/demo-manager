using System;
using System.IO;
using Newtonsoft.Json;

namespace DemoManager.App.Objects
{
    /// <summary>
    ///     Defines method of a demo.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public interface IDemo
    {
        /// <summary>
        ///     Gets the version of this instance.
        /// </summary>
        [JsonProperty]
        GameVersion Version { get; }

        /// <summary>
        ///     Gets the name of this instance.
        /// </summary>
        [JsonProperty]
        string Name { get; }

        /// <summary>
        ///     Gets the UTC date this instance was recorded at.
        /// </summary>
        [JsonProperty]
        DateTime Date { get; }

        /// <summary>
        ///     Gets the map this instance was recorded in.
        /// </summary>
        [JsonProperty]
        string Map { get; }

        /// <summary>
        ///     Gets the mod this instance was recorded in.
        /// </summary>
        [JsonProperty]
        string Mod { get; }

        /// <summary>
        ///     Gets the game type this instance was recorded in.
        /// </summary>
        [JsonProperty]
        string GameType { get; }

        /// <summary>
        ///     Gets the server this instance was recorded on.
        /// </summary>
        [JsonProperty]
        string Server { get; }

        /// <summary>
        ///     Gets the size of the file.
        /// </summary>
        [JsonProperty]
        long Size { get; }

        /// <summary>
        ///     Opens a stream of the demo file.
        /// </summary>
        /// <returns>The stream of the demo file.</returns>
        Stream Open();
    }
}