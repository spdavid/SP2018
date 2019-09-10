using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CallingTheGraph
{
   public class GraphHelper
    {
        private static string tenant = "folkis2018.onmicrosoft.com";
        private static string applicationID = "9837dcca-ba2a-44e7-91cc-4d7545798026";
        private static string thumbPrint = "013B7DCC30A9FEB8937CDB462CA3C14C24A4F0EB";

        public static async Task<string> GetAccessToken()
        {
            var cert = GetCert();

            var realtenant = tenant;
            var realAppId = applicationID;
          
            // string authority = "https://" + tenant;
            string authority = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/", "https://login.windows.net", realtenant);

            var authenticationContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            var cac = new ClientAssertionCertificate(realAppId, cert);
            var authenticationResult = await authenticationContext.AcquireTokenAsync("https://graph.microsoft.com", cac);
            return authenticationResult.AccessToken;
        }

        private static X509Certificate2 GetCert()
        {
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

        public static GraphServiceClient GetGraphClient()
        {
            string accessToken = GetAccessToken().Result;

            GraphServiceClient graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        // Append the access token to the request.
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    }));
            return graphClient;
        }


    }
}
