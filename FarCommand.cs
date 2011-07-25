using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using FarNet;
using System.Text.RegularExpressions;
namespace Shortie
{
    /// <summary>
    /// Command invoked from the command line by the "Shortie:" prefix.
    /// It prints some data depending on the command text after the prefix.
    /// </summary>
    [ModuleCommand(Name = "Far Command", Prefix = "FarC")]
    [Guid("7FB54361-87B8-4CD5-8F0E-2464CE7D190B")]
    public class FarCommand : ModuleCommand
    {
        ShortcutsProvider shortcutProvider = new ShortcutsProvider();

        /// <summary>
        /// This method implements the command action.
        /// The command text is the Command property value.
        /// </summary>
        public override void Invoke(object sender, ModuleCommandEventArgs e)
        {
            try
            {
               
                string command = e.Command.Trim().ToUpper();

                if (ProcessMacros(command))
                    return;
                if (ProcessSpecialCommands(command))
                    return;

                ProcessShortcutCommand(command);
            }
            catch (Exception)
            {
                //TODO Senthil 
            }
        }

        private bool ProcessMacros(string command)
        {
            switch (command)
            {
                case "LEFT":
                    {
                        Far.Net.PostText("macro:post F9 L" + Environment.NewLine, true);
                        return true;
                    }
            }
            return false;
        }

        private void ProcessShortcutCommand(string command)
        {
            //Remove extension. Use Regex becase String.Replace doesnt have option to ignore case.
            string commandAlias = command;
            if (command.EndsWith(Constants.ShortcutExtension, StringComparison.InvariantCultureIgnoreCase))
                commandAlias = Regex.Replace(command, Constants.ShortcutExtension, string.Empty, RegexOptions.IgnoreCase);

            FileInfo fileInfo;
            if (shortcutProvider.ShortCuts.ContainsKey(commandAlias))
                fileInfo = shortcutProvider.ShortCuts[commandAlias];
            else
                fileInfo = ShortcutsProvider.GetTargetFileInfo(command);

            ProcessShortcut(fileInfo);
        }

        public static void ProcessShortcut(FileInfo fileInfo)
        {
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                Far.Net.Panel.CurrentDirectory = fileInfo.FullName;
                Far.Net.Panel.Redraw();
            }
            else
            {
                Far.Net.Panel.GoToPath(fileInfo.FullName);
                Far.Net.Panel.Redraw();
            }
        }

        bool ProcessSpecialCommands(string command)
        {
            switch (command)
            {
                case "SET":
                    {
                        string folder = Far.Net.Panel.CurrentDirectory;

                        SettingProvider provider = new SettingProvider();
                        provider.ShortcutFolderPath = folder;

                        return true;
                    }
                case "CREATE":
                    {
                        shortcutProvider.CreateShortcut(Far.Net.Panel.CurrentDirectory);
                        return true;
                    }

                case "SAME":
                    {
                        Far.Net.Panel2.CurrentDirectory = Far.Net.Panel.CurrentDirectory;
                        Far.Net.Panel2.Redraw();
                        return true;
                    }

            }

            return false;
        }
    }
}
