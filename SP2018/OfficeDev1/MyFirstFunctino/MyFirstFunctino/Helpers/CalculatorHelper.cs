using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstFunctino.Helpers
{
    public class CalculatorHelper
    {

        public static void CalculateItem(int id, ClientContext ctx)
        {
            ListItem item = ctx.Web.Lists.GetByTitle("Calculator").GetItemById(id);

            ctx.Load(item, 
                i => i["number1"], 
                i => i["number2"],
                i => i["operator"],
                i => i["Result"]);
            ctx.ExecuteQuery();

            // if any field is null then end the function
            if (item["number1"] == null || item["number2"] == null || item["operator"] == null)
            {
                return;
            }

            int number1 = int.Parse(item["number1"].ToString());
            int number2 = int.Parse(item["number2"].ToString());
            string opt = item["operator"].ToString();

            int result = 0;

            if (opt == "plus")
            {
                result = number1 + number2;
            }
            else
            {
                result = number1 - number2;
            }

            if (item["Result"] == null || int.Parse(item["Result"].ToString()) != result)
            {
                item["Result"] = result;
                item.Update();
                ctx.ExecuteQuery();
            }

        }

    }
}
