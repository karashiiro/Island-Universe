using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Plugin
{
    public interface IUiBuilder
    {
        event BuildUIDelegate OnBuildUi;
    }
}
