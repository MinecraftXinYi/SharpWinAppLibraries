using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32ini;
using UWPLocalSettingsHelper;
using MsixHelper;
using UnpackagedAppDataPaths;

namespace UniversalSettingStorage
{
    public class UniversalLocalSettings
    {
        IniFile Win32Ini;
        MsixHelp msixHelper = new MsixHelper.MsixHelp();
        bool IsMsixApp;
        UnpackagedApplicationDataPaths unpackagedApplicationDataPaths = new UnpackagedApplicationDataPaths();
        UWPLocalSettings uwpLocalSettings = new UWPLocalSettings();

        public UniversalLocalSettings(int unpackagedAppDataLocationValue = 0, string appPublisher = null, string appName = null)
        {
            string iniPath = unpackagedApplicationDataPaths.UnpackagedAppSettingsINIFullPath(unpackagedAppDataLocationValue, appPublisher, appName);
            Win32Ini = new IniFile(iniPath);
            IsMsixApp = msixHelper.IsMsixEnv();
        }

        public string GetValue(string key, string section = null)
        {
            string getValue = "";
            if (IsMsixApp)
            {
                getValue = uwpLocalSettings.GetValue(key, section);
            }
            else
            {
                getValue = Win32Ini.Read(key, section);
            }
            return getValue;
        }

        public void SetValue(string key, string value, string section = null)
        {
            if (IsMsixApp)
            {
                uwpLocalSettings.SetValue(key, value, section);
            }
            else
            {
                Win32Ini.Write(key, value, section);
            }
        }

        public void RemoveKey(string key, string section = null)
        {
            if (IsMsixApp)
            {
                uwpLocalSettings.RemoveValue(key, section);
            }
            else
            {
                Win32Ini.DeleteKey(key, section);
            }
        }

        public void RemoveSection(string section)
        {
            if (IsMsixApp)
            {
                uwpLocalSettings.RemoveContainer(section);
            }
            else
            {
                Win32Ini.DeleteSection(section);
            }
        }

        public bool KeyExists(string key, string section = null)
        {
            if (IsMsixApp)
            {
                return uwpLocalSettings.ValueExists(key, section);
            }
            else
            {
                return Win32Ini.KeyExists(key, section);
            }
        }
    }
}
