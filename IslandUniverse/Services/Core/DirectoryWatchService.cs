using System;
using System.IO;

namespace IslandUniverse.Services.Core
{
    public class DirectoryWatchService : IDirectoryWatchService
    {
        private readonly FileSystemWatcher fileSystemWatcher;

        public DirectoryWatchService()
        {
            this.fileSystemWatcher = new FileSystemWatcher();
        }

        public void SetAddFileWatch(string path, string filter, Action<string> onAddFile)
        {
            this.fileSystemWatcher.Path = path;
            this.fileSystemWatcher.Filter = filter;
            this.fileSystemWatcher.Created += (sender, e) => onAddFile(e.FullPath);
            this.fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            this.fileSystemWatcher.Dispose();
        }
    }
}
