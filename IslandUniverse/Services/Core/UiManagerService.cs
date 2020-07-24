using ImGuiNET;
using ImGuiScene;
using IslandUniverse.UiStyles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ImGuiScene.SimpleImGuiScene;

namespace IslandUniverse.Services.Core
{
    public class UiManagerService : IUiManagerService, IDisposable
    {
        public const UiStyle DefaultUiStyle = UiStyle.Dark;

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        private SimpleImGuiScene scene;

        public IDictionary<string, ImFontPtr> Fonts { get; }

        public event BuildUIDelegate OnBuildUi;

        public UiManagerService()
        {
            this.Fonts = new Dictionary<string, ImFontPtr>();

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

            this.scene.Renderer.ClearColor = new Vector4(0, 0, 0, 0);

            this.scene.OnBuildUI += OnBuildUi;

            unsafe
            {
                ImFontConfigPtr fontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
                fontConfig.GlyphExtraSpacing.X = 0.5f;
                
                var io = ImGui.GetIO();

                var defaultFontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IslandUniverse.Assets.Roboto-Regular.ttf");
                var dfIntPtr = Marshal.AllocHGlobal((int)defaultFontStream.Length);
                var defaultFont = new UnmanagedMemoryStream((byte*)dfIntPtr.ToPointer(), defaultFontStream.Length, defaultFontStream.Length, FileAccess.Write);
                defaultFontStream.CopyTo(defaultFont);

                // ImGui's font upscaling is pretty bad, so we add some larger versions in advance.
                var fontPtr1 = io.Fonts.AddFontFromMemoryTTF(dfIntPtr, 13, 13f, fontConfig);
                this.Fonts.Add("Roboto Regular 13px", fontPtr1);
                var fontPtr2 = io.Fonts.AddFontFromMemoryTTF(dfIntPtr, 18, 18f, fontConfig);
                this.Fonts.Add("Roboto Regular 18px", fontPtr2);
                var fontPtr3 = io.Fonts.AddFontFromMemoryTTF(dfIntPtr, 24, 24f, fontConfig);
                this.Fonts.Add("Roboto Regular 24px", fontPtr3);

                io.Fonts.Build();

                fontConfig.Destroy();
            }

            SetUiStyle(DefaultUiStyle);

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

        public void SetUiStyle(UiStyle style)
        {
            _ = style switch
            {
                UiStyle.Dark => UiThemeDark.Setup(),
                _ => throw new NotImplementedException(),
            };
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
