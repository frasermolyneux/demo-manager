using System;
using System.IO;

namespace DemoManager.App.Helpers
{
    public static class StreamHelper
    {
        public static void WriteProgressed(this Stream writeStream, Stream readStream, long size,
            Action<int> progressChanged, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            long progress = 0;
            var lastProgress = 0;

            int length;
            do
            {
                length = readStream.Read(buffer, 0, 1024);
                writeStream.Write(buffer, 0, length);

                progress += length;

                if (size > 0)
                {
                    var currentProgress = (int) (100 * progress / size);
                    if (progressChanged != null && lastProgress != currentProgress)
                        progressChanged(size == 0 ? 0 : currentProgress);

                    lastProgress = currentProgress;
                }
            } while (length != 0);
        }
    }
}