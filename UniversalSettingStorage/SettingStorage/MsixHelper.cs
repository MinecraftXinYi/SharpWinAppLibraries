using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MsixHelper
{
    //Msix Helper
    public class MsixHelp
    {
        //Check if this application is an Installed Msix App.
        public bool IsMsixEnv()
        {
            bool get;
            try
            {
                ApplicationDataContainer x = ApplicationData.Current.LocalSettings;
                get = true;
            }
            catch(Exception)
            {
                get = false;
            }
            return get;
        }

    }
}
