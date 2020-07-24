using ImGuiNET;
using IslandUniverse.Services.Agent;
using IslandUniverse.Services.Core;
using IslandUniverse.UiStyles;
using System;
using System.Numerics;

namespace IslandUniverse.Windows
{
    public static partial class MainWindow
    {
        private static WindowState State { get; set; } = WindowState.Index;
        private static UiStyle CurrentStyle { get; set; } = UiManagerService.DefaultUiStyle;

        public static void Draw(IUiManagerService ui, IAgentManagerService agentMan)
        {
            ImGui.SetNextWindowSize(new Vector2(600, 400));
            ImGui.Begin("Island Universe", ImGuiWindowFlags.NoResize);
            _ = State switch
            {
                WindowState.Index => DrawIndex(ui),
                WindowState.AgentIndex => DrawAgentIndex(agentMan),
                WindowState.Settings => DrawSettings(ui),
                _ => throw new NotImplementedException(),
            };
            ImGui.End();
        }

        private enum WindowState
        {
            Index,
            ProcedureIndex,
            ProcedureGraph,
            AgentIndex,
            AgentSetup,
            Settings,
        }
    }
}
