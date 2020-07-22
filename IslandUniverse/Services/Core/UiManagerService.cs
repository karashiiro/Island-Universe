using ImGuiNET;
using ImGuiScene;
using System;
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

            this.scene.Renderer.ClearColor = new Vector4(0, 0, 0, 0);

            this.scene.OnBuildUI += OnBuildUi;

            unsafe
            {
                ImFontConfigPtr fontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
                fontConfig.GlyphExtraSpacing.X = 0.5f;
                
                var io = ImGui.GetIO();

                var defaultFontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IslandUniverse.Assets.Ubuntu-Regular.ttf");
                var dfIntPtr = Marshal.AllocHGlobal((int)defaultFontStream.Length);
                var defaultFont = new UnmanagedMemoryStream((byte*)dfIntPtr.ToPointer(), defaultFontStream.Length, defaultFontStream.Length, FileAccess.Write);
                defaultFontStream.CopyTo(defaultFont);

                io.Fonts.AddFontFromMemoryTTF(dfIntPtr, 13, 13f, fontConfig);
                io.Fonts.Build();

                fontConfig.Destroy();
            }

            // Copy Dalamud's style, it's good
            var style = ImGui.GetStyle();
            style.GrabRounding = 3f;
            style.FrameRounding = 4f;
            style.WindowRounding = 4f;
            style.WindowBorderSize = 0f;

            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.06f, 0.06f, 0.87f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.29f, 0.29f, 0.29f, 0.54f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.54f, 0.54f, 0.54f, 0.40f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.64f, 0.64f, 0.64f, 0.67f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.29f, 0.29f, 0.29f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.19f, 0.19f, 0.19f, 1.00f); // Minor tweak so this doesn't go transparent when collapsed
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.86f, 0.86f, 0.86f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.54f, 0.54f, 0.54f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.67f, 0.67f, 0.67f, 1.00f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.71f, 0.71f, 0.71f, 0.40f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.47f, 0.47f, 0.47f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.74f, 0.74f, 0.74f, 1.00f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.59f, 0.59f, 0.59f, 0.31f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.50f, 0.50f, 0.50f, 0.80f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.79f, 0.79f, 0.79f, 0.25f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.78f, 0.78f, 0.78f, 0.67f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.88f, 0.88f, 0.88f, 0.95f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.23f, 0.23f, 0.23f, 0.86f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.71f, 0.71f, 0.71f, 0.80f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.36f, 0.36f, 0.36f, 1.00f);

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
