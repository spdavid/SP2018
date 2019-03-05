using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using MyFirstSPApp.Helpers;
using OfficeDevPnP.Core;
using MyFirstSPApp.Extensions;

namespace MyFirstSPApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthenticationManager manager = new AuthenticationManager();

            ClientContext ctx = manager.GetSharePointOnlineAuthenticatedContextTenant("https://folkis2018.sharepoint.com/sites/David", "david@folkis2018.onmicrosoft.com", "foo");

            //SPHelper.ShowWebTitle(ctx);
            //SPHelper.ChangeWebTitle(ctx, "Davids Site");
            //SPHelper.GetLists(ctx);
            //SPHelper.CreateList(ctx);
            // SPHelper.CreateListItem(ctx);
            // SPHelper.CreateCarContetType(ctx);

            // SpecialDocument.CreateSpecialDocument(ctx);

            // SPHelper.CreateFieldsFromXMLFile(ctx);
            // SPHelper.CreateFieldsFromProvisioningXML(ctx);
            //SPHelper.GetAllGreenCars(ctx, "Green");

            //SPHelper.CreateTaxonomy(ctx);
            SPHelper.CreatePage(ctx);


            Console.WriteLine("press enter to continue");
            Console.ReadLine();

        }

        // old code


        //Car c = new Car();
        //c.Name = "Tesla";
        //c.Model = "S";

        //Console.WriteLine(c.Model);
        //c.ChangeModel();
        //Console.WriteLine(c.Model);

        //var listTitles = ctx.Web.ShowAllLists();

        //foreach (var item in listTitles)
        //{
        //    Console.WriteLine(item);
        //}

        // not pnp
        //List list = ctx.Web.Lists.GetByTitle("Documents");
        //ctx.Load(list);
        //ctx.ExecuteQuery();

        //// pnp way
        //List list2 = ctx.Web.GetListByTitle("Documents");


    }
}
