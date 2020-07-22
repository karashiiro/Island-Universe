using Newtonsoft.Json;
using System.IO;

namespace IslandUniverse.Services.Core
{
    public class ConfigurationManagerService : IConfigurationManagerService
    {
        private string configDir;

        public void Initialize(string configDir)
        {
            this.configDir = configDir;
        }

        public T GetConfiguration<T>(string configName) where T : class
        {
            var configPath = Path.Combine(this.configDir, configName);
            if (!File.Exists(configPath)) return null;
            return JsonConvert.DeserializeObject<T>(configPath);
        }

        public void SaveConfiguration(string configName, object config)
        {
            var configPath = Path.Combine(this.configDir, configName);
            File.WriteAllText(configPath, JsonConvert.SerializeObject(config));
        }
    }
}
