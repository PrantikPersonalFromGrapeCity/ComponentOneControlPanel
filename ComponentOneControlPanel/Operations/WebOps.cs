using GrapeCity.Tools.ComponentOneControlPanel.Containers;
using GrapeCity.Tools.ComponentOneControlPanel.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCity.Tools.ComponentOneControlPanel.Operations
{
   internal class WebOps
    {
        internal delegate void ProgressCompletedEventHandler(object sender, EventArgs e);
        internal event ProgressCompletedEventHandler ProgressCompleted;
        internal void ReadDataFromServer(bool withProxy = false)
        {
            var prdJson = App.Current.Resources["ProductsJson"] as string;
            var failArg = new WebJsonArg
            {
                Failuretype = WebJsonFailureEnum.FailureInReadingResource,
                Success = false
            };
            var succArg = new WebJsonArg
            {

                Success = true
            };
            if (!withProxy)
            {
                
                if (prdJson == null)
                {
                   
                    OnProgressCompleted(failArg);
                    return;
                }
            }
            failArg.Failuretype = WebJsonFailureEnum.FailureInFetchingFromWeb;
            try
            {
                using (var client = new C1WebClient())
                {
                    if (withProxy)
                    {
                        if (!string.IsNullOrEmpty(GlobalVariables.ProxyUri))
                        {
                            var prxy = new WebProxy(GlobalVariables.ProxyUri, true)
                            {
                                Credentials = new NetworkCredential(GlobalVariables.DomainUser, GlobalVariables.DomainUserPassword)
                            };
                            client.Proxy = prxy;

                        }
                    }
                    var responseString = client.DownloadString(prdJson);
                    var json = JsonConvert.DeserializeObject<dynamic>(responseString);
                    GlobalVariables.ProductsJson = json as JObject;
                   
                    OnProgressCompleted(succArg);
                    return;

                }
            }
            catch
            {
                if (withProxy)
                {
                    
                    OnProgressCompleted(failArg);
                    return;
                }
                var proxyUrl = string.Empty;
                var proxyPort = 0;
                if(!RegistryOps.CheckIfProxy(out proxyUrl, out proxyPort))
                {
                    OnProgressCompleted(failArg);
                    return;
                }
                if (!CredentialOps.GetCredential(proxyUrl,proxyPort))
                {
                    OnProgressCompleted(failArg);
                    return;
                }
                ReadDataFromServer(true);
            }
          
        }
        protected virtual void OnProgressCompleted(EventArgs e)
        {
            ProgressCompleted?.Invoke(this, e);
        }
    }
}
