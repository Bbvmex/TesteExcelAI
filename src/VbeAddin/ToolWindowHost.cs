using System;
using System.Runtime.InteropServices;
using Microsoft.Vbe.Interop;
using VbeAddin.UI;

namespace VbeAddin
{
    internal class ToolWindowHost : IDisposable
    {
        // Stable GUID for VBE to persist window position between sessions
        private const string WindowGuid = "{A1B2C3D4-E5F6-7890-ABCD-EF1234567891}";
        private const string WindowCaption = "AI Assistant";

        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private Window _toolWindow;
        private bool _disposed;

        public ToolWindowHost(VBE vbe, AddIn addIn)
        {
            _vbe = vbe;
            _addIn = addIn;
        }

        public void Show()
        {
            object controlInstance = null;

            _toolWindow = _vbe.Windows.CreateToolWindow(
                _addIn,
                "VbeAddin.AiAssistantControl",
                WindowCaption,
                WindowGuid,
                ref controlInstance);

            if (_toolWindow == null)
                throw new InvalidOperationException(
                    "CreateToolWindow returned null. Verify that VbeAddin.AiAssistantControl is COM-registered.");

            DockBesideProjectExplorer();
            _toolWindow.Visible = true;
        }

        private void DockBesideProjectExplorer()
        {
            try
            {
                // {9ED54F84-...} is the well-known GUID for the VBE Project Explorer window
                Window projectExplorer = _vbe.Windows.Item("{9ED54F84-A89D-4fcd-A854-44251E925F09}");
                _toolWindow.SetKind((vbext_WindowType)12); // 12 = vbext_wt_LinkedWindow
                projectExplorer.LinkedWindowFrame.LinkedWindows.Add(_toolWindow);
            }
            catch
            {
                // Docking is best-effort — the window still opens floating if this fails
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                if (_toolWindow != null)
                {
                    _toolWindow.Close();
                    Marshal.ReleaseComObject(_toolWindow);
                    _toolWindow = null;
                }
            }
            catch { }
        }
    }
}
