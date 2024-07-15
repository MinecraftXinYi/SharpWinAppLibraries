using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPLocalSettingsHelper
{
    public class UWPLocalSettings
    {
        ApplicationDataContainer localSettings;
        public string GetValue(string key, string section = null)
        {
            localSettings = ApplicationData.Current.LocalSettings;
            string getValue = "";
            if (string.IsNullOrEmpty(section))
            {
                if (localSettings.Values.ContainsKey(key))
                {
                    getValue = localSettings.Values[key].ToString();
                }
            }
            else
            {
                ApplicationDataContainer subLocalSettings = localSettings.CreateContainer(section, ApplicationDataCreateDisposition.Always);
                if (subLocalSettings.Values.ContainsKey(key))
                {
                    getValue = subLocalSettings.Values[key].ToString();
                }
            }
            return getValue;
        }

        public void SetValue(string key, string value, string section = null)
        {
            localSettings = ApplicationData.Current.LocalSettings;
            if (string.IsNullOrEmpty(section))
            {
                localSettings.Values[key] = value;
            }
            else
            {
                ApplicationDataContainer subLocalSettings = localSettings.CreateContainer(section, ApplicationDataCreateDisposition.Always);
                subLocalSettings.Values[key] = value;
            }
        }

        public void RemoveValue(string key, string section = null)
        {
            localSettings = ApplicationData.Current.LocalSettings;
            if (string.IsNullOrEmpty(section))
            {
                if (localSettings.Values.ContainsKey(key))
                {
                    localSettings.Values.Remove(key);
                }
            }
            else
            {
                ApplicationDataContainer subLocalSettings = localSettings.CreateContainer(section, ApplicationDataCreateDisposition.Always);
                if (subLocalSettings.Values.ContainsKey(key))
                {
                    subLocalSettings.Values.Remove(key);
                }
            }
        }

        public void RemoveContainer(string section)
        {
            localSettings = ApplicationData.Current.LocalSettings;
            localSettings.DeleteContainer(section);
        }

        public bool ValueExists(string key, string section = null)
        {
            localSettings = ApplicationData.Current.LocalSettings;
            if (string.IsNullOrEmpty(section))
            {
                return localSettings.Values.ContainsKey(key);
            }
            else
            {
                ApplicationDataContainer subLocalSettings = localSettings.CreateContainer(section, ApplicationDataCreateDisposition.Always);
                return subLocalSettings.Values.ContainsKey(key);
            }
        }
    }
}
