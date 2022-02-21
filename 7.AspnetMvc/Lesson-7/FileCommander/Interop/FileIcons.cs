using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FileCommander.Interop;
public static class FileIcons
{
    private static Icon? _folderLargeIcon;

    private static Icon? _folderSmallIcon;

    public static Icon FolderLarge => _folderLargeIcon ?? (_folderLargeIcon = GetStockIcon(SHSIID_FOLDER, SHGSI_LARGEICON));

    public static Icon FolderSmall => _folderSmallIcon ?? (_folderSmallIcon = GetStockIcon(SHSIID_FOLDER, SHGSI_SMALLICON));

    public static Icon? GetIcon(string path)
    {
        return ExtractFromPath(path);
    }

    private static Icon? ExtractFromPath(string path)
    {
        SHFILEINFO shinfo = new SHFILEINFO();
        SHGetFileInfo(
            path,
            0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
            SHGFI_ICON | SHGFI_SMALLICON);

        if (shinfo.hIcon == IntPtr.Zero)
            return null;

        var icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone(); // Get a copy that doesn't use the original handle
        DestroyIcon(shinfo.hIcon); // Clean up native icon to prevent resource leak
        return (icon);
    }

    private static Icon GetStockIcon(uint type, uint size)
    {
        var info = new SHSTOCKICONINFO();
        info.cbSize = (uint)Marshal.SizeOf(info);

        SHGetStockIconInfo(type, SHGSI_ICON | size, ref info);

        var icon = (Icon)Icon.FromHandle(info.hIcon).Clone(); // Get a copy that doesn't use the original handle
        DestroyIcon(info.hIcon); // Clean up native icon to prevent resource leak

        return icon;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct SHSTOCKICONINFO
    {
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysIconIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

    [DllImport("shell32.dll")]
    private static extern int SHGetStockIconInfo(uint siid, uint uFlags, ref SHSTOCKICONINFO psii);

    [DllImport("shell32.dll")]
    private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool DestroyIcon(IntPtr handle);

    private const uint SHGFI_ICON = 0x100;

    private const uint SHGFI_LARGEICON = 0x0;

    private const uint SHGFI_SMALLICON = 0x1;

    private const uint SHSIID_FOLDER = 0x3;

    private const uint SHGSI_ICON = 0x100;

    private const uint SHGSI_LARGEICON = 0x0;

    private const uint SHGSI_SMALLICON = 0x1;
}
