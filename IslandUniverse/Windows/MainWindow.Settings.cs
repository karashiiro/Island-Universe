using ImGuiNET;
using IslandUniverse.Services.Core;
using IslandUniverse.UiStyles;
using System;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static bool DrawSettings(IUiManagerService ui)
        {
            var fontPtr = ui.Fonts["Roboto Regular 18px"];

            ImGui.PushFont(fontPtr);
            ImGui.Text("Settings");
            ImGui.SetWindowFontScale(14f / 18f);
            ImGui.Text("Appearance");
            ImGui.SetWindowFontScale(1f);
            ImGui.PopFont();

            var themeOptions = Enum.GetNames(typeof(UiStyle));
            var selectedStyle = (int)CurrentStyle;
            if (ImGui.Combo("Style", ref selectedStyle, themeOptions, themeOptions.Length))
            {
                ui.SetUiStyle((UiStyle)selectedStyle);
                CurrentStyle = (UiStyle)selectedStyle;
            }

            if (ImGui.Button("Done##Settings"))
            {
                State = WindowState.AgentIndex;
            }

            return true;
        }
    }
}
