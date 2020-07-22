using IslandUniverse.Agents;
using IslandUniverse.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IslandUniverse.Services.Plugin
{
    public class PluginManagerService : IPluginManagerService
    {
        private readonly IServiceProvider container;
        private readonly List<PluginRuntimeInformation> loadedPlugins;

        public IEnumerable<IIslandUniversePlugin> LoadedPlugins => this.loadedPlugins.Select(plugin => plugin.Instance);

        public IEnumerable<Type> LoadedAgentTypes => this.loadedPlugins
            .SelectMany(plugin => plugin.Instance.GetType().Assembly.GetTypes()
                .Where(type => type.GetInterface(typeof(IAgent).Name) != null));

        public PluginManagerService(IServiceProvider container)
        {
            this.container = container;
            this.loadedPlugins = new List<PluginRuntimeInformation>();
        }

        public void LoadPluginAssembly(string pluginPath)
        {
            var plugins = LoadPlugins(pluginPath);
            if (plugins != null)
            {
                this.loadedPlugins.AddRange(plugins);
            }
        }

        private IList<PluginRuntimeInformation> LoadPlugins(string pluginPath)
        {
            var pluginLoadContext = new PluginLoadContext(pluginPath);
            var assembly = pluginLoadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
            if (assembly == null) return null;

            var types = assembly.GetTypes();
            var plugins = types.Where(type => type.GetInterface(typeof(IIslandUniversePlugin).Name) != null);
            if (!plugins.Any()) return null;

            var pluginInstances = new List<PluginRuntimeInformation>();
            foreach (var pluginType in plugins)
            {
                var instance = (IIslandUniversePlugin)Activator.CreateInstance(pluginType);
                var pluginInterface = new IslandUniversePluginInterface(this.container, instance.UniqueName);
                instance.Initialize(pluginInterface);
                pluginInstances.Add(new PluginRuntimeInformation
                {
                    LoadContext = pluginLoadContext,
                    Instance = instance,
                    UniqueName = instance.UniqueName,
                });
            }

            return pluginInstances;
        }

        public void UnloadPluginAssembly(string uniqueName, out WeakReference alcWeakRef)
        {
            var pluginLoadContext = this.loadedPlugins.FirstOrDefault(plugin => plugin.UniqueName == uniqueName).LoadContext;
            var plugins = this.loadedPlugins.Where(plugin => plugin.LoadContext == pluginLoadContext);
            this.loadedPlugins.RemoveAll(plugin => plugin.LoadContext == pluginLoadContext);
            foreach (var plugin in plugins)
            {
                plugin.Instance.Dispose();
            }

            alcWeakRef = new WeakReference(pluginLoadContext, trackResurrection: true);
            pluginLoadContext.Unload();

            /* Can wait on a full unload with this if the plugin is actually being uninstalled
            for (int i = 0; alcWeakRef.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            } 
            */
        }

        private class PluginRuntimeInformation
        {
            public PluginLoadContext LoadContext { get; set; }
            public IIslandUniversePlugin Instance { get; set; }
            public string UniqueName { get; set; }
        }
    }
}
