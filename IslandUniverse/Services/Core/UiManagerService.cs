using ImGuiNET;
using ImGuiScene;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Services.Core
{
    public class UiManagerService : IUiManagerService, IDisposable
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        private SimpleImGuiScene scene;

        public event BuildUIDelegate OnBuildUi;

        public UiManagerService()
        {
            BuildTrayIcon();
        }

        public void CreateScene(string iniPath = null)
        {
            this.scene = new SimpleImGuiScene(RendererFactory.RendererBackend.OpenGL3, new WindowCreateInfo
            {
                Title = "ImGui Overlay",
                Fullscreen = true,
                TransparentColor = new float[] { 0, 0, 0, 0 },
            })
            {
                ImGuiIniPath = iniPath,
            };

            this.scene.OnBuildUI += ImGui.ShowDemoWindow;
            this.scene.OnBuildUI += OnBuildUi;

            this.scene.Run();
        }

        private void BuildTrayIcon()
        {
            this.trayMenu = new ContextMenuStrip();
            this.trayMenu.Items.Add("Show Everything", null, Show);
            this.trayMenu.Items.Add("Hide Everything", null, Hide);
            this.trayMenu.Items.Add("Exit", null, Exit);

            trayIcon = new NotifyIcon
            {
                Text = "Island Universe",
                Icon = new Icon(SystemIcons.Application, 40, 40)
            };

            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
        }

        private void Show(object sender, EventArgs e)
        {
            ShowWindow(scene.Window.GetHWnd(), SW_SHOW);
        }

        private void Hide(object sender, EventArgs e)
        {
            ShowWindow(scene.Window.GetHWnd(), SW_HIDE);
        }

        private void Exit(object sender, EventArgs e)
        {
            this.scene.ShouldQuit = true;
        }

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public void Dispose()
        {
            this.scene.Dispose();
        }
    }
}
