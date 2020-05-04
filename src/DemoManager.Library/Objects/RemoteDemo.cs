using System;
using System.IO;
using DemoManager.Library.Helpers;

namespace DemoManager.Library.Objects
{
    /// <summary>
    ///     Contains information about a demo stored in the remote repository.
    /// </summary>
    public class RemoteDemo : IDemo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteDemo" /> class.
        /// </summary>
        /// <param name="info">Sourced from the redirect Json data.</param>
        public RemoteDemo(dynamic info)
        {
            Version = info.Version;
            Name = info.Name;
            Date = info.Date;
            Map = info.Map;
            Mod = info.Mod;
            GameType = info.GameType;
            Server = info.Server;

            if (info.Size != null)
                Size = info.Size;

            Path = "http://redirect.xtremeidiots.net/demo/files/" + info.FileName;
        }

        /// <summary>
        ///     Gets the path to the file.
        /// </summary>
        public string Path { get; }

        #region Overrides of Object

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Implementation of IDemo

        /// <summary>
        ///     Gets the version of this instance.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     Gets the name of this instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the UTC date this instance was recorded at.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        ///     Gets the map this instance was recorded in.
        /// </summary>
        public string Map { get; }

        /// <summary>
        ///     Gets the mod this instance was recorded in.
        /// </summary>
        public string Mod { get; }

        /// <summary>
        ///     Gets the game type this instance was recorded in.
        /// </summary>
        public string GameType { get; }

        /// <summary>
        ///     Gets the server this instance was recorded on.
        /// </summary>
        public string Server { get; }

        /// <summary>
        ///     Gets the size of the file.
        /// </summary>
        public long Size { get; }

        /// <summary>
        ///     Opens a stream of the demo file.
        /// </summary>
        /// <returns>The stream of the demo file.</returns>
        public Stream Open()
        {
            return RedirectHelper.DownloadFile(Path);
        }

        #endregion

        #region Equality members

        protected bool Equals(RemoteDemo other)
        {
            return Version == other.Version && string.Equals(Name, other.Name) && Date.Equals(other.Date) &&
                   string.Equals(Map, other.Map) && string.Equals(Mod, other.Mod) && Size == other.Size &&
                   string.Equals(Server, other.Server) && string.Equals(GameType, other.GameType);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
        ///     <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RemoteDemo) obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Version;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                hashCode = (hashCode * 397) ^ (Map != null ? Map.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Mod != null ? Mod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Size.GetHashCode();
                hashCode = (hashCode * 397) ^ (Server != null ? Server.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GameType != null ? GameType.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}