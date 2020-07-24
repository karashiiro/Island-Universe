using ImGuiNET;
using IslandUniverse.Services.Agent;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static bool DrawAgentIndex(IAgentManagerService agentMan)   
        {
            ImGui.BeginChild("##MainWindowColumnHolder1", new Vector2(800, 300));
            {
                ImGui.Columns(2, "##MainWindowColumns1", border: false);
                {
                }
                ImGui.GetWindowDrawList().AddLine(
                    ImGui.GetWindowPos() + new Vector2(392, 20),
                    ImGui.GetWindowPos() + new Vector2(392, 350),
                    ImGui.GetColorU32(ImGuiCol.Border));
                ImGui.NextColumn();
                {
                }
            }
            ImGui.EndChild();

            return true;
        }
    }
}
