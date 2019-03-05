using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace MyFirstSPApp.Extensions
{
    public static class Extensions
    {
        public static void ChangeModel(this Car c)
        {
            c.Model = "X";
        }

        public static List<string> ShowAllLists(this Web web)
        {
            ClientRuntimeContext ctx = web.Context;
            ctx.Load(web.Lists);
            ctx.ExecuteQuery();

            var listTitles = web.Lists.Select(l => l.Title).ToList();
            return listTitles;
        }

    }
}
