using ImGuiNET;
using System.Collections.Generic;
using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Plugin
{
    public interface IUiBuilder
    {
        public IDictionary<string, ImFontPtr> Fonts { get; }

        event BuildUIDelegate OnBuildUi;
    }
}
