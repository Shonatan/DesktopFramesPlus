using System.Runtime.InteropServices;

// Hand-written interop for the Windows Script Host Object Model (wshom.ocx),
// replacing the tlbimp-generated COMReference wrapper. The COMReference approach
// requires the .NET Framework MSBuild (ResolveComReference is unsupported in
// `dotnet build`, error MSB4803), which blocks CLI and CI builds. These
// declarations mirror the shapes tlbimp generates, so existing call sites
// (`new WshShell()`, `(IWshShortcut)shell.CreateShortcut(...)`) compile unchanged.
// GUIDs and DISPIDs come from the wshom.ocx type library (IWshRuntimeLibrary).
namespace IWshRuntimeLibrary
{
    [ComImport]
    [Guid("F935DC21-1CF0-11D0-ADB9-00C04FD58A0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IWshShell
    {
        [DispId(1002)]
        IWshShortcut CreateShortcut([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
    }

    [ComImport]
    [Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IWshShortcut
    {
        [DispId(0)]
        string FullName { [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(1000)]
        string Arguments { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1001)]
        string Description { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1002)]
        string Hotkey { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1003)]
        string IconLocation { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1004)]
        string RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1005)]
        string TargetPath { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(1006)]
        int WindowStyle { get; [param: In] set; }

        [DispId(1007)]
        string WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] get; [param: In, MarshalAs(UnmanagedType.BStr)] set; }

        [DispId(2000)]
        void Load([In, MarshalAs(UnmanagedType.BStr)] string PathLink);

        [DispId(2001)]
        void Save();
    }

    // Coclass for WScript.Shell; `new WshShell()` resolves here via the CoClass
    // attribute, matching the tlbimp-generated pattern.
    [ComImport]
    [Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")]
    [ClassInterface(ClassInterfaceType.None)]
    public class WshShellClass
    {
    }

    [ComImport]
    [Guid("F935DC21-1CF0-11D0-ADB9-00C04FD58A0B")]
    [CoClass(typeof(WshShellClass))]
    public interface WshShell : IWshShell
    {
    }
}
