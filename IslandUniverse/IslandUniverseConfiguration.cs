using IslandUniverse.Services.Core;
using Newtonsoft.Json;

namespace IslandUniverse
{
    public class IslandUniverseConfiguration
    {
        public IslandUniverseConfiguration()
        {
        }

        [JsonIgnore] private IConfigurationManagerService configurationManager;

        public void Initialize(IConfigurationManagerService configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        public void Save()
        {
            this.configurationManager.SaveConfiguration(IslandUniverse.ConfigurationFileName, this);
        }
    }
}
