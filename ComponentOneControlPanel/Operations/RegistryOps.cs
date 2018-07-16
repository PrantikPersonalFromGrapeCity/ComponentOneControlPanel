using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCity.Tools.ComponentOneControlPanel.Operations
{
  internal static  class RegistryOps
    {
        /// <summary>
        /// Checks registry to find if proxy is enabled
        /// returns proxy Url and port
        /// </summary>
        /// <param name="proxyUrl"></param>
        /// <param name="proxyPort"></param>
        /// <returns></returns>
        internal static bool CheckIfProxy(out string proxyUrl, out int proxyPort)
        {
            proxyUrl = "";
            proxyPort = 0;
            try
            {
                var regKey = Registry.CurrentUser.OpenSubKey(App.Current.Resources["INTERNETSETTINGSREG"] as string, false);
                if (regKey == null) return false;
                var proxyEnabled = Convert.ToBoolean(regKey.GetValue("ProxyEnable"));
                if (!proxyEnabled) return false;
                var proxystring = regKey.GetValue("ProxyServer").ToString();
                var prxySplits = proxystring.Split(new char[] { ':' }, StringSplitOptions.None);
                proxyUrl = prxySplits[0];
                proxyPort = int.Parse(prxySplits[1]);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
