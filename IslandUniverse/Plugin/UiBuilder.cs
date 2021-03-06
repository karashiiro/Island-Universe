﻿using ImGuiNET;
using IslandUniverse.Services.Core;
using System;
using System.Collections.Generic;
using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Plugin
{
    public class UiBuilder : IUiBuilder, IDisposable
    {
        private readonly IUiManagerService uiManager;

        public IDictionary<string, ImFontPtr> Fonts => this.uiManager.Fonts;

        public event BuildUIDelegate OnBuildUi;

        public UiBuilder(IUiManagerService uiManager)
        {
            this.uiManager = uiManager;
            this.uiManager.OnBuildUi += Draw;
        }

        private void Draw()
        {
            var windows = OnBuildUi;
            windows?.Invoke();
        }

        public void Dispose()
        {
            this.uiManager.OnBuildUi -= Draw;
        }
    }
}
