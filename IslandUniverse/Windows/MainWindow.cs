using ImGuiNET;
using IslandUniverse.Services.Core;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static class MainWindow
    {
        public static void Draw(IUiManagerService ui)
        {
            ImGui.SetNextWindowSize(new Vector2(600, 400));
            ImGui.Begin("Island Universe", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar);
            {
                var textColor = ImGui.GetColorU32(ImGuiCol.Text);

                var fontPtr = ui.Fonts["Roboto Regular 18px"];
                ImGui.PushFont(fontPtr);
                ImGui.Text("Welcome to Island Universe!");
                ImGui.PopFont();

                ImGui.PushStyleColor(ImGuiCol.Button, 0);
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, ImGui.GetColorU32(new Vector4(1, 1, 1, 0.3f)));
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ImGui.GetColorU32(new Vector4(1, 1, 1, 0.1f)));

                ImGui.BeginChild("##MainWindowColumnHolder1", new Vector2(800, 400));
                {
                    ImGui.Columns(2, "##MainWindowColumns1", border: false);
                    {
                        ImGui.Text("Recent Procedure Executions:");
                        ImGui.BeginChild("##MainWindowColumnHolder2", new Vector2(368, 300));
                        {
                            ImGui.Button("", new Vector2(360, 18));
                            ImGui.GetWindowDrawList().AddText(ImGui.GetWindowPos() + new Vector2(10, 2), textColor, "Timestamp 1");
                            ImGui.GetWindowDrawList().AddText(ImGui.GetWindowPos() + new Vector2(170, 2), textColor, "Procedure 1");
                        }
                        ImGui.EndChild();
                    }
                    ImGui.GetWindowDrawList().AddLine(
                        ImGui.GetWindowPos() + new Vector2(392, 20),
                        ImGui.GetWindowPos() + new Vector2(392, 350),
                        ImGui.GetColorU32(ImGuiCol.Border));
                    ImGui.NextColumn();
                    {
                        ImGui.PushFont(fontPtr);
                        ImGui.SetWindowFontScale(14f / 18f);

                        ImGui.PushStyleVar(ImGuiStyleVar.ButtonTextAlign, new Vector2());

                        ImGui.NewLine();
                        ImGui.Button("Agents", new Vector2(160, 22));
                        ImGui.Button("Procedures", new Vector2(160, 22));
                        ImGui.Button("Settings", new Vector2(160, 22));

                        ImGui.PopStyleVar();

                        ImGui.SetWindowFontScale(1f);
                        ImGui.PopFont();
                    }
                }
                ImGui.EndChild();

                ImGui.PopStyleColor(3);
            }
            ImGui.End();
        }
    }
}
