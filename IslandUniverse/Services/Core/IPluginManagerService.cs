using IslandUniverse.Plugin;
using System;
using System.Collections.Generic;
using System.Runtime.Loader;

namespace IslandUniverse.Services.Core
{
    public interface IPluginManagerService
    {
        /// <summary>
        ///     A copy of the list of currently-loaded plugins.
        /// </summary>
        public IEnumerable<IIslandUniversePlugin> LoadedPlugins { get; }

        /// <summary>
        ///     Load plugins from an assembly using a specified path.
        /// </summary>
        /// <param name="pluginPath"></param>
        void LoadPluginAssembly(string pluginPath);

        /// <summary>
        ///     Unload a plugin assembly using the unique name of the contained plugin.
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <param name="alcWeakRef">A weak reference to the <see cref="AssemblyLoadContext"/> used for the plugin.</param>
        void UnloadPluginAssembly(string uniqueName, out WeakReference alcWeakRef);
    }
}
