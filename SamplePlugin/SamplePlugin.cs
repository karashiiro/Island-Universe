using IslandUniverse.Plugin;

namespace SamplePlugin
{
    public class SamplePlugin : IIslandUniversePlugin
    {
        public string DisplayName => "Sample Plugin";

        public string UniqueName => "SamplePlugin";

        public string Description => "A sample plugin.";

        private IslandUniversePluginInterface pluginInterface;

        public void Initialize(IslandUniversePluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Dispose()
        {
            //
        }
    }
}
