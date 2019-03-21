using Microsoft.Graph;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace ADPermissions.Helper
{
    public class SiteCreationHelper
    {
        public static void CreateOffice365Group()
        {
            CreateGraphGroup("dav#####idsgröäå oup12");
        }

        private async static void CreateGraphGroup(string title)
        {

            string nick = UrlFriendlyString(title);
            string accessToken = ContextHelper.GetAccessToken().Result;


            GraphServiceClient graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        // Append the access token to the request.
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    }));



            Microsoft.Graph.Group addedGroup = null;

           var existinggroups = await graphClient.Groups.Request().Filter("mailNickName eq '" + nick + "'").GetAsync();

            if (existinggroups.Count > 0)
            {
                addedGroup = existinggroups[0];
            }

            if (addedGroup == null)
            {
                var newGroup = new Microsoft.Graph.Group
                {
                    DisplayName = title,
                    Description = title,
                    MailNickname = nick,
                    MailEnabled = true,
                    SecurityEnabled = false,
                    Visibility = "Private",
                    GroupTypes = new List<string> { "Unified" }
                };

                addedGroup = await graphClient.Groups.Request().AddAsync(newGroup);

                var user = await graphClient.Users["david@folkis2018.onmicrosoft.com"].Request().GetAsync();
                await graphClient.Groups[addedGroup.Id].Owners.References.Request().AddAsync(user);

            }

            string webUrl = "";
            int seconds = 0;
            do
            {
                Thread.Sleep(2000);
                seconds += 2;
                try
                {
                    var site = await graphClient.Groups[addedGroup.Id].Sites["root"].Request().Select("webUrl").GetAsync();
                    webUrl = site.WebUrl;
                }
                catch (Exception)
                {
                    Console.WriteLine("not found at " + seconds);
                    if (seconds > 30)
                    {
                        break;
                    }
                }

            } while (webUrl == "");
            Console.WriteLine(webUrl);
            using (ClientContext ctx = ContextHelper.GetContext(webUrl))
            {
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();

                Console.WriteLine(ctx.Web.Title);

            }





        }

        public static string UrlFriendlyString(string siteTitle)
        {
            Regex removebleChars = new Regex(@"[#%\*:<>.,\?/\\|{}~+'\[\]&]");
            Regex replaceableChars = new Regex($"[åäâ]");

            string cleanedString = removebleChars.Replace(siteTitle.ToLowerInvariant(), "");
            cleanedString = replaceableChars
                        .Replace(cleanedString, "a")
                        .Replace("ö", "o")
                        .Replace("ñ", "n")
                        .Replace("ü", "u")
                        .Replace("é", "e")
                        .Replace("î", "i");
            cleanedString = cleanedString.Replace(" ", "");

            return cleanedString;
        }




    }
}
