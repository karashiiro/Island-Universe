using IslandUniverse.Services.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace IslandUniverse.Plugin
{
    public class IslandUniversePluginInterface : IDisposable
    {
        private readonly string pluginName;

        private readonly IConfigurationManagerService configurationManager;
        private readonly UiBuilder uiBuilder;

        /// <summary>
        ///     ImGui draw hook.
        /// </summary>
        public IUiBuilder UiBuilder { get; private set; }

        /// <summary>
        ///     Sound object factory for plugin developer convenience.
        /// </summary>
        public ISoundManager SoundManager { get; private set; }

        /// <summary>
        ///     The shared <see cref="HttpClient"/> instance across the application. Don't dispose of this yourself.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        public IslandUniversePluginInterface(IServiceProvider container, string pluginName)
        {
            this.configurationManager = container.GetRequiredService<IConfigurationManagerService>();
            this.uiBuilder = new UiBuilder(container.GetRequiredService<IUiManagerService>());
            this.pluginName = pluginName;

            UiBuilder = this.uiBuilder;
            HttpClient = container.GetRequiredService<HttpClient>();
        }

        /// <summary>
        ///     Gets the saved configuration for this plugin, returning null if it does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetConfiguration<T>() where T : class
        {
            return this.configurationManager.GetConfiguration<T>(this.pluginName + ".json");
        }

        /// <summary>
        ///     Saves the provided configuration object to be loaded later.
        /// </summary>
        /// <param name="config"></param>
        public void SaveConfiguration(object config)
        {
            this.configurationManager.SaveConfiguration(this.pluginName + ".json", config);
        }

        public void Dispose()
        {
            this.uiBuilder.Dispose();
        }
    }
}
