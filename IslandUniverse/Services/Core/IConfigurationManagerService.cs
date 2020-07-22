namespace IslandUniverse.Services.Core
{
    public interface IConfigurationManagerService
    {
        /// <summary>
        ///     Initializes the service.
        /// </summary>
        /// <param name="configDir"></param>
        void Initialize(string configDir);

        /// <summary>
        ///     Gets a configuration file of the specified type, returning <see cref="null"/> if none exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configName"></param>
        /// <returns></returns>
        T GetConfiguration<T>(string configName) where T : class;

        /// <summary>
        ///     Saves the configuration object specified.
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="config"></param>
        void SaveConfiguration(string configName, object config);
    }
}
