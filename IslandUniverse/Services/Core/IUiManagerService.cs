using ImGuiNET;
using System.Collections.Generic;
using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Services.Core
{
    public interface IUiManagerService
    {
        /// <summary>
        ///     Loaded fonts.
        /// </summary>
        IDictionary<string, ImFontPtr> Fonts { get; }

        /// <summary>
        ///     Interface builder hook for ImGui.
        /// </summary>
        event BuildUIDelegate OnBuildUi;

        /// <summary>
        ///     Create the ImGui overlay.
        /// </summary>
        /// <param name="iniPath">The path to the ImGui ini file location.</param>
        void CreateScene(string iniPath);
    }
}
