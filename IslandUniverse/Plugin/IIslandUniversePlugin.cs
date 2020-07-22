using System;

namespace IslandUniverse.Plugin
{
    public interface IIslandUniversePlugin : IDisposable
    {
        string DisplayName { get; }
        string UniqueName { get; }
        string Description { get; }

        void Initialize(IslandUniversePluginInterface pluginInterface);
    }
}
