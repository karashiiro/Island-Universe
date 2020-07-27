using ImGuiNET;
using IslandUniverse.Agents;
using IslandUniverse.Services.Agent;
using IslandUniverse.Services.Plugin;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static int selectedAgentType;

        private static bool DrawAgentSetup(IPluginManagerService pluginMan)
        {
            var properties = CurrentAgent.GetType().GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Static
                | BindingFlags.Instance
                | BindingFlags.Public);

            ImGui.BeginChild("##MainWindowColumnHolder3", new Vector2(800, 330));
            {
                ImGui.Columns(2, "##MainWindowColumns3", border: false);
                {
                    ImGui.Combo("", ref selectedAgentType, pluginMan.LoadedAgentTypes
                        .Select(agent => (string)agent.GetProperty("AgentTypeName").GetValue(null)).ToArray(), pluginMan.LoadedAgentTypes.Count());

                    foreach (var property in properties.Where(prop => prop.GetCustomAttribute<AgentEditableAttribute>(true) != null))
                    {
                        ImGui.Text(property.Name);
                        var temp = property.GetValue(CurrentAgent);
                        if (property.PropertyType == typeof(string))
                        {
                            var tempString = (string)temp ?? string.Empty;
                            if (ImGui.InputText($"##{property.Name}", ref tempString, 3500000))
                                property.SetMethod.Invoke(CurrentAgent, new[] { tempString });
                        }
                    }

                    if (ImGui.Button("Done##AgentSetup"))
                    {
                        State = WindowState.AgentIndex;
                    }
                }
                ImGui.GetWindowDrawList().AddLine(
                    ImGui.GetWindowPos() + new Vector2(392, 42),
                    ImGui.GetWindowPos() + new Vector2(392, 322),
                    ImGui.GetColorU32(ImGuiCol.Border));
                ImGui.NextColumn();
                {
                    ImGui.BeginChild("##AgentSetupDescription", new Vector2(200, 330));
                    var property = properties.FirstOrDefault(prop => prop.Name == "AgentDescription");
                    ImGui.TextWrapped((string)property.GetValue(null));
                    ImGui.EndChild();
                }
            }
            ImGui.EndChild();
            return true;
        }
    }
}
