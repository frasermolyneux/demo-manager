using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoManager.Library.Objects;

namespace DemoManager.Library.Helpers
{
    // this class will interface between the local demo repository and also the remote demo repository

    public interface IDemoRepository
    {
        /// <summary>
        ///     Gets the demos in this <see cref="IDemoRepository" />.
        /// </summary>
        IEnumerable<IDemo> Demos { get; }

        /// <summary>
        ///     Stores the specified demo in this <see cref="IDemoRepository" />.
        /// </summary>
        /// <param name="demo">The demo.</param>
        /// <param name="progressChanged">Callback called when the progress has changed.</param>
        Task<IDemo> Store(IDemo demo, Action<int> progressChanged);
    }
}