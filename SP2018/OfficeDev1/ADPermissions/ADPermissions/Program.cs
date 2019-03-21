using ADPermissions.Helper;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ADPermissions
{
    class Program
    {
        static void Main(string[] args)
        {
            string siteUrl = ConfigurationManager.AppSettings["siteUrl"];

            var ctx = ContextHelper.GetContext(siteUrl);
            ctx.Load(ctx.Web);
            ctx.ExecuteQuery();

            SiteCreationHelper.CreateOffice365Group();

            Console.WriteLine(ctx.Web.Title);


           // List list = ctx.Web.GetListByTitle("SampleList");

           // Console.WriteLine(list.CurrentChangeToken.StringValue);


           //var lastCheckedString = list.GetPropertyBagValueString("lastchecked", null);



           // ChangeQuery query = new ChangeQuery(false, false);
           // query.Item = true;
           // query.Add = true;
           // query.Update = true;
           // query.DeleteObject = true;
           // if (lastCheckedString != null)
           // {
           //     ChangeToken token = new ChangeToken();
           //     token.StringValue = lastCheckedString;
           //     query.ChangeTokenStart = token;
           // }
           // query.ChangeTokenEnd = list.CurrentChangeToken;

           // ChangeCollection changes = list.GetChanges(query);

           // ctx.Load(changes);
           // ctx.ExecuteQuery();

           // foreach (ChangeItem change in changes)
           // {
           //     Console.WriteLine(change.ItemId);
           //     Console.WriteLine(change.ChangeType.ToString());
               
           // }

           // list.SetPropertyBagValue("lastchecked", list.CurrentChangeToken.StringValue);



            Console.ReadLine();

        }


        //        string thumbPrint = "23183403021E240DD6968BFB75B28D54909F476E";
        //        string siteUrl = "https://folkis2018.sharepoint.com/sites/david";
        //        string tenant = "folkis2018.onmicrosoft.com";
        //        string applicationID = "4ec29f20-e269-405d-a2de-674fa10d095c";

        //        X509Certificate2 cert2 = null;
        //        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //            try
        //            {
        //                store.Open(OpenFlags.ReadOnly);

        //                var col = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, false);

        //                if (col == null || col.Count == 0)
        //                {


        //                }
        //                cert2 = col[0];

        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            finally
        //            {
        //                store.Close();
        //            }

        //            OfficeDevPnP.Core.AuthenticationManager authmanager = new OfficeDevPnP.Core.AuthenticationManager();

        //            using (ClientContext ctx = authmanager.GetAzureADAppOnlyAuthenticatedContext(siteUrl, applicationID, tenant, cert2))
        //            {
        //                ctx.Load(ctx.Web);
        //                ctx.ExecuteQuery();

        //                Console.WriteLine(ctx.Web.Title);
        //                Console.ReadLine();

        //                List list = ctx.Web.CreateList(ListTemplateType.GenericList, "fakelist", false);

        //ListItem item = list.AddItem(new ListItemCreationInformation());
        //item["Title"] = "yo";
        //                item.Update();

        //                ctx.ExecuteQuery();


        //            }
    }
}
