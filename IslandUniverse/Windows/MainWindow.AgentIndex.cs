using ImGuiNET;
using IslandUniverse.Services.Agent;
using IslandUniverse.Services.Core;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static bool DrawAgentIndex(IUiManagerService ui, IAgentManagerService agentMan)   
        {
            var fontPtr = ui.Fonts["Roboto Regular 18px"];

            ImGui.BeginChild("##MainWindowColumnHolder1", new Vector2(800, 330));
            {
                ImGui.Columns(2, "##MainWindowColumns1", border: false);
                {
                }
                ImGui.GetWindowDrawList().AddLine(
                    ImGui.GetWindowPos() + new Vector2(392, 42),
                    ImGui.GetWindowPos() + new Vector2(392, 322),
                    ImGui.GetColorU32(ImGuiCol.Border));
                ImGui.NextColumn();
                {
                    ImGui.PushFont(fontPtr);
                    ImGui.SetWindowFontScale(14f / 18f);

                    ImGui.PushStyleColor(ImGuiCol.Button, 0);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive, ImGui.GetColorU32(new Vector4(1, 1, 1, 0.3f)));
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ImGui.GetColorU32(new Vector4(1, 1, 1, 0.1f)));

                    ImGui.PushStyleVar(ImGuiStyleVar.ButtonTextAlign, new Vector2());

                    ImGui.NewLine();
                    ImGui.NewLine();
                    ImGui.Spacing();
                    if (ImGui.Button("New Agent", new Vector2(160, 22)))
                    {
                    }

                    ImGui.PopStyleVar();
                    ImGui.PopStyleColor(3);
                    ImGui.SetWindowFontScale(1f);
                    ImGui.PopFont();
                }
            }
            ImGui.EndChild();

            return true;
        }
    }
}
