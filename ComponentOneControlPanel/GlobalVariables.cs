using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCity.Tools.ComponentOneControlPanel
{
   internal class GlobalVariables
    {
        internal static JObject ProductsJson { get; set; }
        internal static string ProxyUri = "";// WebProxyUri
        internal static string DomainUser = "";//Domain User
        internal static string DomainUserPassword = "";//Domain User Password
    }
}
