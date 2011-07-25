using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FarNet;

namespace Shortie
{
    class ShortcutsProvider
    {
        public Dictionary<string, FileInfo> ShortCuts { get; set; }

        readonly string shortcutFolder;

        public ShortcutsProvider()
        {
            SettingProvider settings = new SettingProvider();
            shortcutFolder = settings.ShortcutFolderPath;
            ShortCuts = new Dictionary<string, FileInfo>();
            InitShortcuts();
        }

        void InitShortcuts()
        {
            try
            {
                if (!Directory.Exists(shortcutFolder))
                    return;

                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();

                var files = Directory.GetFiles(shortcutFolder, "*.lnk");
                foreach (var file in files)
                {
                    try
                    {
                        var shortcutFileInfo = new FileInfo(file);
                        var targetFileInfo = GetTargetFileInfo(file);
                        ShortCuts.Add(shortcutFileInfo.Name.Replace(shortcutFileInfo.Extension, string.Empty).ToUpper(), targetFileInfo);
                    }
                    catch (Exception)
                    {
                        //TODO Senthil
                    }
                }
            }
            catch (Exception)
            {
                //TODO Senthil 
            }
        }

        public static FileInfo GetTargetFileInfo(string shortcutPath)
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            var link = shell.CreateShortcut(shortcutPath);
            var targetFileInfo = new FileInfo(link.TargetPath as string);
            return targetFileInfo;
        }

        internal void CreateShortcut(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            string shortcurtFilePath = Path.Combine(shortcutFolder, fileInfo.Name+ ".lnk");

            if (File.Exists(shortcurtFilePath))
                Far.Net.Message("Shortcut already exist");
            var link = shell.CreateShortcut(shortcurtFilePath);
            link.TargetPath = fileInfo.FullName;
            link.Save();
        }
    }
}
