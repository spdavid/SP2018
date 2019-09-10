using Microsoft.Azure.WebJobs;
using MyFirstDurableFunction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstDurableFunction.DurableFunction
{
    public class Sequence
    {

        [FunctionName("CombineAllUserNames")]
        public static async Task<string[]> CombineAllUserNames(
            [OrchestrationTrigger] DurableOrchestrationContextBase context)
        {

            var input = context.GetInput<List<Person>>();

            var tasks = new List<Task<string>>();

            foreach (var item in input)
            {
                tasks.Add(context.CallActivityAsync<string>("GetFullName", item));
            }

           string[] finalVals = await Task.WhenAll(tasks);
            return finalVals;
           


            //RetryOptions options = new RetryOptions(TimeSpan.FromSeconds(5), 1);

            //context.SetCustomStatus("add david");
            //string n1 = await context.CallActivityWithRetryAsync<string>("GetFullName", options, new Person() {FirstName = "David", LastName = "Opdendries" });
            //context.SetCustomStatus("add John");
            //string n2 = await context.CallActivityWithRetryAsync<string>("GetFullName", options, new Person() { FirstName = "John", LastName = "Doe" });
            //context.SetCustomStatus("add Jane");
            //string n3 = await context.CallActivityWithRetryAsync<string>("GetFullName", options, new Person() { FirstName = "Jane", LastName = "Doe" });

            //var names = new List<string>();
            //names.Add(n1);
            //names.Add(n2);
            //names.Add(n3);





            //return names;
        }




    }
}
