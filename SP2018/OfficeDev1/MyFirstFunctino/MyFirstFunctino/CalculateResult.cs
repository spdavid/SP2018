using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using MyFirstFunctino.Helpers;

namespace MyFirstFunctino
{
    public static class CalculateResult
    {
        [FunctionName("CalculateResult")]
        public static void Run([QueueTrigger("newitem", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            string siteUrl = Environment.GetEnvironmentVariable("siteUrl");
            using (ClientContext ctx = ContextHelper.GetContext(siteUrl))
            {
                int id = int.Parse(myQueueItem);
                CalculatorHelper.CalculateItem(id, ctx);
            }

        }
    }
}
