using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using MyFirstFunctino.Helpers;

namespace MyFirstFunctino
{
    public static class ProcessItem
    {
        [FunctionName("ProcessItem")]
        public static void Run([QueueTrigger("processitem", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"Do my processing for : {myQueueItem}");

            ClientContext ctx = ContextHelper.GetContext("https://folkis2018.sharepoint.com/sites/David/");

           List list = ctx.Web.Lists.GetByTitle("SampleList");

           ListItem item = list.GetItemById(int.Parse(myQueueItem));
            ctx.Load(item, i => i["Title"]);
            ctx.ExecuteQuery();

            log.Info("Title is :" + item["Title"].ToString());

        }
    }
}
