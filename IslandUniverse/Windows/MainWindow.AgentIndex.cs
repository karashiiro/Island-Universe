using ImGuiNET;
using IslandUniverse.Agents;
using IslandUniverse.Agents.Builtin;
using IslandUniverse.Services.Agent;
using IslandUniverse.Services.Core;
using System.Net.Http;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static bool DrawAgentIndex(IUiManagerService ui, IAgentManagerService agentMan, HttpClient http)   
        {
            var fontPtr = ui.Fonts["Roboto Regular 18px"];

            ImGui.BeginChild("##MainWindowColumnHolder2", new Vector2(800, 330));
            {
                ImGui.Columns(2, "##MainWindowColumns2", border: false);
                {
                    ImGui.Text(agentMan.Count == 1 ? "1 agent" : $"{agentMan.Count} agents");
                    ImGui.BeginChild("##AgentIndex", new Vector2(600, 310));
                    {
                        for (var i = 0; i < agentMan.Count; i++)
                        {
                            DrawCard(agentMan[i], i);
                            if ((i + 1) % 5 != 0)
                            {
                                ImGui.SameLine();
                            }
                        }
                        DrawNewAgentCard(agentMan, http);
                    }
                    ImGui.EndChild();
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
                    if (ImGui.Button("Back", new Vector2(160, 22)))
                    {
                        State = WindowState.Index;
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

        private static void DrawNewAgentCard(IAgentManagerService agentMan, HttpClient http)
        {
            var textColor = ImGui.GetColorU32(ImGuiCol.Text);
            var plusColor = ImGui.GetColorU32(new Vector4(0.1f, 0.1f, 0.1f, 1.0f));

            ImGui.BeginChild("##NewAgentCard", new Vector2(65, 80));
            {
                var drawList = ImGui.GetWindowDrawList();
                var windowPos = ImGui.GetWindowPos();

                if (ImGui.Button("", new Vector2(65, 80)))
                {
                    var newAgent = new HttpStatusAgent(http);
                    agentMan.Add(newAgent);
                    CurrentAgent = newAgent;
                    State = WindowState.AgentSetup;
                }
                drawList.AddText(windowPos + new Vector2(4, 60), textColor, "New Agent");
                drawList.AddLine(windowPos + new Vector2(32f, 10f), windowPos + new Vector2(32f, 55f), plusColor, 10f);
                drawList.AddLine(windowPos + new Vector2(10f, 32f), windowPos + new Vector2(55f, 32f), plusColor, 10f);
            }
            ImGui.EndChild();
        }

        private static void DrawCard(AgentBase agent, int id)
        {
            var textColor = ImGui.GetColorU32(ImGuiCol.Text);

            ImGui.BeginChild($"##{agent.AgentTypeName}{id}", new Vector2(65, 80));
            {
                var drawList = ImGui.GetWindowDrawList();
                var windowPos = ImGui.GetWindowPos();

                if (ImGui.Button("", new Vector2(65, 80)))
                {
                    CurrentAgent = agent;
                    State = WindowState.AgentSetup;
                }
                drawList.AddText(windowPos + new Vector2(4, 60), textColor, agent.Name);
            }
            ImGui.EndChild();
        }
    }
}
