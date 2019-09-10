using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using MyFirstDurableFunction.Models;
using System.Collections.Generic;

namespace MyFirstDurableFunction.DurableFunction
{
    public static class SomeTrigger
    {
        [FunctionName("SomeTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log, [OrchestrationClient] DurableOrchestrationClientBase starter)
        {
            var people = new List<Person>() {
                new Person() { FirstName = "David", LastName = "Opdendries" },
                new Person() { FirstName = "David2", LastName = "Opdendries" },
                new Person() { FirstName = "David3", LastName = "Opdendries" },
                new Person() { FirstName = "David4", LastName = "Opdendries" },
                new Person() { FirstName = "David5", LastName = "Opdendries" },
                new Person() { FirstName = "David6", LastName = "Opdendries" }
            };


           var instanceId = await starter.StartNewAsync("CombineAllUserNames", people);

           return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
