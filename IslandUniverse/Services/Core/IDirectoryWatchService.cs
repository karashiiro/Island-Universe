using System;

namespace IslandUniverse.Services.Core
{
    public interface IDirectoryWatchService : IDisposable
    {
        /// <summary>
        ///     Sets the class instance to scan for file additions to the directory specified.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filter"></param>
        /// <param name="onAddFile"></param>
        void SetAddFileWatch(string path, string filter, Action<string> onAddFile);
    }
}
