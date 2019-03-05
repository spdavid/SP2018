using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MyFirstFunctino.Helpers;
using MyFirstFunctino.Models;
using Newtonsoft.Json;

namespace MyFirstFunctino
{
    public static class GetChanges
    {
        [FunctionName("GetChanges")]
        public static void Run([QueueTrigger("achangeismade", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            NotificationModel notification= JsonConvert.DeserializeObject<NotificationModel>(myQueueItem);

            ClientContext ctx = ContextHelper.GetContext("https://folkis2018.sharepoint.com/sites/David");


            List list = ctx.Web.GetListByTitle("SampleList");
            var lastCheckedString = list.GetPropertyBagValueString("lastchecked", null);

            ChangeQuery query = new ChangeQuery(false, false);
            query.Item = true;
            query.Add = true;
            query.Update = true;
            //query.DeleteObject = true; dont care
            if (lastCheckedString != null)
            {
                ChangeToken token = new ChangeToken();
                token.StringValue = lastCheckedString;
                query.ChangeTokenStart = token;
            }
            query.ChangeTokenEnd = list.CurrentChangeToken;

            ChangeCollection changes = list.GetChanges(query);

            ctx.Load(changes);
            ctx.ExecuteQuery();

            foreach (ChangeItem change in changes)
            {
                log.Info(change.ItemId.ToString());
                log.Info(change.ChangeType.ToString());

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
                // Get queue... create if does not exist.
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                CloudQueue queue = queueClient.GetQueueReference("processitem");
                queue.CreateIfNotExists();

                queue.AddMessage(new CloudQueueMessage(change.ItemId.ToString()));

            }

            list.SetPropertyBagValue("lastchecked", list.CurrentChangeToken.StringValue);




        }
    }
}
