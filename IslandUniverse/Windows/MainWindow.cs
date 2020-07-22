using ImGuiNET;
using IslandUniverse.Services.Core;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static class MainWindow
    {
        public static void Draw(IPluginManagerService pluginManager, string storageDir)
        {
            ImGui.SetNextWindowSize(new Vector2(300, 400));
            ImGui.Begin("Island Universe", ImGuiWindowFlags.NoResize);

            ImGui.TextWrapped($"Storage directory: {storageDir}");
            ImGui.Text("Loaded plugins:");
            foreach (var plugin in pluginManager.LoadedPlugins)
            {
                ImGui.Text(plugin.DisplayName);
            }

            ImGui.End();
        }
    }
}
