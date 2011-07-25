using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarNet;

namespace Shortie
{
    class SettingProvider
    {
        const string shortcutFolderRegisterKey = @"Plugins\FarNet.Modules\Shortie\ShortcutFolder";
       
        public bool IsExist()
        {
            return !string.IsNullOrEmpty(ShortcutFolderPath);
        }

        public string ShortcutFolderPath
        {
            get
            {
                using (var key = Far.Net.OpenRegistryKey(shortcutFolderRegisterKey, true))
                {
                    string shortcutFoldePath = key.GetValue(string.Empty, string.Empty) as string;
                    return shortcutFoldePath.Trim();
                }
            }
            set
            {
                using (var key = Far.Net.OpenRegistryKey(shortcutFolderRegisterKey, true))
                {
                    key.SetValue(string.Empty, value);
                }
            }
        }
    }
}
