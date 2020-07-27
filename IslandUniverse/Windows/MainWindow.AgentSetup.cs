using ImGuiNET;
using IslandUniverse.Agents;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static bool DrawAgentSetup()
        {
            var properties = CurrentAgent.GetType().GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Instance
                | BindingFlags.Public);

            ImGui.BeginChild("##MainWindowColumnHolder3", new Vector2(800, 330));
            {
                ImGui.Columns(2, "##MainWindowColumns3", border: false);
                {
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
                }
                ImGui.GetWindowDrawList().AddLine(
                    ImGui.GetWindowPos() + new Vector2(392, 42),
                    ImGui.GetWindowPos() + new Vector2(392, 322),
                    ImGui.GetColorU32(ImGuiCol.Border));
                ImGui.NextColumn();
                {
                    ImGui.BeginChild("##AgentSetupDescription", new Vector2(200, 330));
                    var property = properties.FirstOrDefault(prop => prop.Name == "AgentDescription");
                    ImGui.TextWrapped((string)property.GetValue(CurrentAgent));
                    ImGui.EndChild();
                }
            }
            ImGui.EndChild();
            return true;
        }
    }
}
