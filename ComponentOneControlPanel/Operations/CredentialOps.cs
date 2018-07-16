using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCity.Tools.ComponentOneControlPanel.Operations
{
  internal static class CredentialOps
    {
        /// <summary>
        /// Loads the Credentialmanagement dll
        /// Gets the username and password associated with the url
        /// </summary>
        /// <param name="target"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        internal static bool GetCredential(string target, int port)
        {

            try
            {

                var bts = Properties.Resources.CredentialManagement;
                var assemBly = Assembly.Load(bts, null);
                Type cred = assemBly.GetType("CredentialManagement.Credential");
                if (cred != null)
                {
                    var instance = Activator.CreateInstance(cred, null);
                    var bindingflags = BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;
                    cred.GetProperty("Target").SetValue(instance, target, null);
                    var mi = cred.GetMethod("Load", bindingflags);
                    if (mi != null)
                    {
                        var invoked = mi.Invoke(instance, null);
                        if (invoked != null)
                        {
                            var username = cred.GetProperty("Username").GetValue(instance, null);
                            if (username != null) GlobalVariables.DomainUser = username.ToString();
                            var password = cred.GetProperty("Password").GetValue(instance, null);
                            if (password != null) GlobalVariables.DomainUserPassword = password.ToString();
                            GlobalVariables.ProxyUri = string.Format("{0}:{1}", target, port.ToString());

                        }

                    }
                }
            }
            catch
            {
                GlobalVariables.ProxyUri =
                    GlobalVariables.DomainUser =
                    GlobalVariables.DomainUserPassword = string.Empty;
                return false;
            }




            return true;


        }
    }
}
