using System;
using System.Runtime.InteropServices;
using Extensibility;
using Microsoft.Vbe.Interop;

namespace VbeAddin
{
    [ComVisible(true)]
    [Guid("A1B2C3D4-E5F6-7890-ABCD-EF1234567890")]
    [ProgId("VbeAddin.Connect")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Connect : IDTExtensibility2
    {
        private VBE _vbe;
        private AddIn _addIn;
        private ToolWindowHost _toolWindowHost;

        public void OnConnection(object Application, ext_ConnectMode ConnectMode,
                                 object AddInInst, ref Array custom)
        {
            _vbe    = (VBE)Application;
            _addIn  = (AddIn)AddInInst;

            _toolWindowHost = new ToolWindowHost(_vbe, _addIn);
            _toolWindowHost.Show();
        }

        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
        {
            _toolWindowHost?.Dispose();
            _toolWindowHost = null;

            if (_addIn  != null) { Marshal.ReleaseComObject(_addIn);  _addIn  = null; }
            if (_vbe    != null) { Marshal.ReleaseComObject(_vbe);    _vbe    = null; }
        }

        public void OnAddInsUpdate(ref Array custom)   { }
        public void OnStartupComplete(ref Array custom) { }
        public void OnBeginShutdown(ref Array custom)   { }
    }
}
