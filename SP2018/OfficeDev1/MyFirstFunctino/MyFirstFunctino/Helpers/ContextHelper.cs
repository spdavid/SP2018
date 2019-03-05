using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstFunctino.Helpers
{
    public class ContextHelper
    {
        public static ClientContext GetContext(string siteUrl)
        {
            string thumbPrint = Environment.GetEnvironmentVariable("thumbPrint");
            string tenant = Environment.GetEnvironmentVariable("tenant"); 
            string applicationID = Environment.GetEnvironmentVariable("applicationID");

            X509Certificate2 cert2 = null;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);
                if (col == null || col.Count == 0)
                {
                }
                cert2 = col[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                store.Close();
            }

            OfficeDevPnP.Core.AuthenticationManager authmanager = new OfficeDevPnP.Core.AuthenticationManager();
            return authmanager.GetAzureADAppOnlyAuthenticatedContext(siteUrl, applicationID, tenant, cert2);
        }
    }
}
