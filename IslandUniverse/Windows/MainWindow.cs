using ImGuiNET;
using IslandUniverse.Services.Core;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static class MainWindow
    {
        public static void Draw(IUiManagerService ui)
        {
            ImGui.SetNextWindowSize(new Vector2(600, 450));
            ImGui.Begin("Island Universe", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar);
            {
                var fontPtr1 = ui.Fonts["Roboto Regular 18px"];
                ImGui.PushFont(fontPtr1);
                ImGui.Text("Welcome to Island Universe!");
                ImGui.PopFont();

                ImGui.BeginChild("##MainWindowColumnHolder1", new Vector2(800, 400));
                {
                    ImGui.Columns(2, "##MainWindowColumns1", border: false);
                    {
                        ImGui.Text("Recent Procedure Executions:");
                        ImGui.BeginChild("##MainWindowColumnHolder2", new Vector2(368, 300));
                        {
                            ImGui.Columns(2, "##MainWindowColumns2", border: false);
                            {
                                ImGui.Text("Timestamp");
                            }
                            ImGui.NextColumn();
                            {
                                ImGui.Text("Procedure 1");
                            }
                        }
                        ImGui.EndChild();
                    }
                    ImGui.GetWindowDrawList().AddLine(
                        ImGui.GetWindowPos() + new Vector2(380, 20),
                        ImGui.GetWindowPos() + new Vector2(380, 350),
                        ImGui.GetColorU32(ImGuiCol.Border));
                    ImGui.NextColumn();
                    {
                        var fontPtr2 = ui.Fonts["Roboto Regular 18px"];
                        ImGui.PushFont(fontPtr2);
                        ImGui.SetWindowFontScale(14f / 18f);
                        ImGui.NewLine();
                        ImGui.Text("Agents");
                        ImGui.Text("Procedures");
                        ImGui.Text("Settings");
                        ImGui.SetWindowFontScale(1f);
                        ImGui.PopFont();
                    }
                }
                ImGui.EndChild();
            }
            ImGui.End();
        }
    }
}
