using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stu.ShortcutCreators
{
    public static class ShortcutCreator
    {
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink { }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        private interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                         int cch, IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void SetHotkey(short wHotkey);
            void SetShowCmd(int iShowCmd);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        public static void CreateShortcutOnDesktop(string shortcutName, string exePath)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, shortcutName + ".lnk");

            if (File.Exists(shortcutPath))
                return;

            var shellLink = (IShellLink)new ShellLink();
            shellLink.SetPath(exePath);
            shellLink.SetDescription(shortcutName);
            shellLink.SetWorkingDirectory(Path.GetDirectoryName(exePath));

            var file = (System.Runtime.InteropServices.ComTypes.IPersistFile)shellLink;
            file.Save(shortcutPath, false);
        }
    }
}