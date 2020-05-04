using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoManager.Library.Objects;

namespace DemoManager.Library.Helpers
{
    public class LocalDemoRepository : IDemoRepository
    {
        #region Implementation of IDemoRepository

        /// <summary>
        ///     Gets the demos in this <see cref="IDemoRepository" />.
        /// </summary>
        public IEnumerable<IDemo> Demos
        {
            get { return Factory.GameDetection.GetInstalledGames().SelectMany(game => game.Demos); }
        }

        /// <summary>
        ///     Stores the specified demo in this <see cref="IDemoRepository" />.
        /// </summary>
        /// <param name="demo">The demo.</param>
        /// <param name="progressChanged">Callback called when the progress has changed.</param>
        /// <exception cref="System.ArgumentNullException">demo</exception>
        /// <exception cref="System.Exception">Game is not installed</exception>
        public Task<IDemo> Store(IDemo demo, Action<int> progressChanged)
        {
            if (demo == null) throw new ArgumentNullException("demo");

            var game = demo.Version.GetGame();

            if (game == null)
                throw new Exception("Game is not installed");

            var localPath = game.GetDemoStorageFolder(demo);

            // Ensure the local directory exists.
            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);

            // Make sure the file doesn't already exist.
            var filename = string.Format("{0}.{1}", demo.Name, game.GetDemoExtension());

            if (File.Exists(Path.Combine(localPath, filename)))
            {
                // If the file does already exist, check if the creation date is different.
                if (demo.Date == File.GetCreationTimeUtc(Path.Combine(localPath, filename)))
                    return Task.FromResult<IDemo>(new LocalDemo(Path.Combine(localPath, filename), demo.Version));

                // If the files are different, make the name unique.
                var attempts = 0;
                do
                {
                    filename = string.Format("{0}.{2}.{1}", demo.Name, game.GetDemoExtension(), attempts);
                    attempts++;
                } while (File.Exists(Path.Combine(localPath, filename)));
            }

            return Task.Run(() =>
            {
                // Write the demo to the local path.
                using (var readStream = demo.Open())
                using (var writeStream = File.OpenWrite(Path.Combine(localPath, filename)))
                {
                    writeStream.WriteProgressed(readStream, demo.Size, progressChanged);
                }

                // Set the creation date of the file to match the recording date of the demo.
                File.SetCreationTimeUtc(Path.Combine(localPath, filename), demo.Date);

                return (IDemo) new LocalDemo(Path.Combine(localPath, filename), demo.Version);
            });
        }

        #endregion
    }
}