using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ADPermissions.Helper
{
    public class ContextHelper
    {

        public static ClientContext GetContext(string siteUrl)
        {
            string thumbPrint = ConfigurationManager.AppSettings["thumbPrint"];
            

            string tenant = ConfigurationManager.AppSettings["tenant"];
            string applicationID = ConfigurationManager.AppSettings["applicationID"];

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

        public static async Task<string> GetAccessToken()
        {
            string tenant = ConfigurationManager.AppSettings["tenant"];
            string applicationID = ConfigurationManager.AppSettings["applicationID"];
            var cert = GetCert();

            // string authority = "https://" + tenant;
            string authority = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/", "https://login.windows.net", tenant);

            var authenticationContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            var cac = new ClientAssertionCertificate(applicationID, cert);
            var authenticationResult = await authenticationContext.AcquireTokenAsync("https://graph.microsoft.com", cac);
            return authenticationResult.AccessToken;
        }

        private static X509Certificate2 GetCert()
        {
            string thumbPrint = ConfigurationManager.AppSettings["thumbPrint"];


            X509Certificate2 cert2 = null;
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                var col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);

                if (col == null || col.Count == 0)
                {

                    return null;
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

            return cert2;
        }

    }
}
