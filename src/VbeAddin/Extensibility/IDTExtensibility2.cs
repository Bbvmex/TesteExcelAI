using System.Runtime.InteropServices;

// Inline declaration of IDTExtensibility2 — avoids requiring the Extensibility
// COM type library to be registered on the build machine.
namespace Extensibility
{
    public enum ext_ConnectMode
    {
        ext_cm_AfterStartup   = 0,
        ext_cm_Startup        = 1,
        ext_cm_External       = 2,
        ext_cm_CommandLine    = 3,
    }

    public enum ext_DisconnectMode
    {
        ext_dm_HostShutdown   = 0,
        ext_dm_UserClosed     = 1,
    }

    [ComImport]
    [Guid("B65AD801-ABAF-11D0-BB8B-00A0C90F2744")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IDTExtensibility2
    {
        void OnConnection(
            [MarshalAs(UnmanagedType.IDispatch)] object Application,
            ext_ConnectMode ConnectMode,
            [MarshalAs(UnmanagedType.IDispatch)] object AddInInst,
            ref System.Array custom);

        void OnDisconnection(
            ext_DisconnectMode RemoveMode,
            ref System.Array custom);

        void OnAddInsUpdate(ref System.Array custom);

        void OnStartupComplete(ref System.Array custom);

        void OnBeginShutdown(ref System.Array custom);
    }
}
