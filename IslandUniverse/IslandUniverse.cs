using IslandUniverse.Services.Agent;
using IslandUniverse.Services.Core;
using IslandUniverse.Services.Plugin;
using IslandUniverse.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace IslandUniverse
{
    public class IslandUniverse
    {
        public const string ConfigurationFileName = "IslandUniverse.json";

        private readonly IServiceProvider container;

        private readonly string storageDirOverride;

        private IslandUniverseConfiguration config;

        private string StorageDir => this.storageDirOverride ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IslandUniverse");
        private string PluginDir => Path.Combine(StorageDir, "InstalledPlugins");
        private string ConfigDir => Path.Combine(StorageDir, "Configuration");

        public IslandUniverse(string storageDirOverride = null)
        {
            this.container = BuildServiceProvider();
            this.storageDirOverride = storageDirOverride != null ? Path.GetFullPath(storageDirOverride) : null;

            Directory.CreateDirectory(StorageDir);
        }

        public void Start()
        {
            LoadConfiguration();
            LoadPlugins();
            WatchPluginFolder();
            InitializeScene(); // Application run state, won't proceed until UI exits
        }

        private void LoadConfiguration()
        {
            Directory.CreateDirectory(ConfigDir);

            var configManager = this.container.GetRequiredService<IConfigurationManagerService>();
            configManager.Initialize(ConfigDir);
            this.config = configManager.GetConfiguration<IslandUniverseConfiguration>(ConfigurationFileName)
                ?? new IslandUniverseConfiguration();
            this.config.Initialize(configManager);
        }

        private void LoadPlugins()
        {
            var pluginManager = this.container.GetRequiredService<IPluginManagerService>();

            Directory.CreateDirectory(PluginDir);

            var pluginPaths = Directory.GetFiles(PluginDir);
            foreach (var pluginPath in pluginPaths)
            {
                pluginManager.LoadPluginAssembly(pluginPath);
            }
        }

        private void WatchPluginFolder()
        {
            var pluginManager = this.container.GetRequiredService<IPluginManagerService>();

            var fw = new FileSystemWatcher
            {
                Path = PluginDir,
                Filter = "*dll",
            };

            fw.Created += (sender, e) => pluginManager.LoadPluginAssembly(e.FullPath);
            fw.EnableRaisingEvents = true;
        }

        private void InitializeScene()
        {
            var ui = this.container.GetRequiredService<IUiManagerService>();
            var agentMan = this.container.GetRequiredService<IAgentManagerService>();
            var http = this.container.GetRequiredService<HttpClient>();

            ui.OnBuildUi += () => MainWindow.Draw(ui, agentMan, http);
            
            ui.CreateScene(iniPath: Path.Combine(StorageDir, "IslandUniverseImGui.ini"));
        }

        private IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton<IUiManagerService, UiManagerService>()
                .AddSingleton<IPluginManagerService, PluginManagerService>()
                .AddSingleton<IConfigurationManagerService, ConfigurationManagerService>()
                .AddSingleton<IAgentManagerService, AgentManagerService>()
                .AddSingleton<IProcedureManagerService, ProcedureManagerService>()
                .AddSingleton<IDiagnosticsService, DiagnosticsService>()

                .AddSingleton<HttpClient>()

                .BuildServiceProvider();
        }
    }
}
