using System.IO;
using System.Windows.Forms;
using System;
using Stu.ShortcutCreators;

namespace Stu
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string shortcutName = "SchoolSkill"; 
            string shortcutPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                shortcutName + ".lnk");

            if (!File.Exists(shortcutPath))
            {
                string exePath = Application.ExecutablePath;
                ShortcutCreator.CreateShortcutOnDesktop(shortcutName, exePath);
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}