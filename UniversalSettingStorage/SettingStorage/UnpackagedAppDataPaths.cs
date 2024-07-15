using MsixHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UnpackagedAppDataPaths
{
    internal class UnpackagedApplicationDataPaths
    {
        private string UnpackagedAppDataContainerPath(int unpackagedAppDataLocationValue = 0, string appPublisher = null, string appName = null)
        {
            string folderPartialBasePath1;
            string folderPartialBasePath2;
            string folderBasePath;

            if (string.IsNullOrEmpty(appName))
            {
                appName = "Default";
            }

            if (!string.IsNullOrEmpty(appPublisher))
            {
                folderPartialBasePath2 = $"{appPublisher}/{appName}";
            }
            else
            {
                folderPartialBasePath2 = $"{appName}";
            }

            if (unpackagedAppDataLocationValue >= 1)
            {
                if (unpackagedAppDataLocationValue >= 2)
                {
                    folderPartialBasePath1 = System.AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    folderPartialBasePath1 = Environment.GetEnvironmentVariable("APPDATA");
                }
            }
            else
            {
                folderPartialBasePath1 = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            }

            folderBasePath = $"{folderPartialBasePath1}/Unpackaged/{folderPartialBasePath2}";

            Directory.CreateDirectory(folderBasePath);
            return folderBasePath;
        }

        private string UnpackagedAppDataContainerFullPath(int unpackagedAppDataLocationValue = 0, string appPublisher = null, string appName = null)
        {
            string folderBasePath;
            string folderFullPath;

            folderBasePath = UnpackagedAppDataContainerPath(unpackagedAppDataLocationValue, appPublisher, appName);
            folderFullPath = Path.GetFullPath(folderBasePath);

            return folderFullPath;
        }

        public string UnpackagedAppSettingsINI(int unpackagedAppDataLocationValue = 0, string appPublisher = null, string appName = null)
        {
            string folderBasePath;
            string iniBasePath;

            folderBasePath = UnpackagedAppDataContainerPath(unpackagedAppDataLocationValue, appPublisher, appName);
            iniBasePath = $"{folderBasePath}/Settings/settings.ini";

            Directory.CreateDirectory($"{folderBasePath}/Settings");
            return iniBasePath;
        }

        public string UnpackagedAppSettingsINIFullPath(int unpackagedAppDataLocationValue = 0, string appPublisher = null, string appName = null)
        {
            string iniBasePath;
            string iniFullPath;

            iniBasePath = UnpackagedAppSettingsINI(unpackagedAppDataLocationValue, appPublisher, appName);
            iniFullPath = Path.GetFullPath(iniBasePath);

            return iniFullPath;
        }


    }
}
