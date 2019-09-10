using Microsoft.Azure.WebJobs;
using MyFirstDurableFunction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyFirstDurableFunction.DurableFunction
{
    public class Tasks
    {
        [FunctionName("GetFullName")]
        public static string GetFullName([ActivityTrigger] Person info)
        {
            Thread.Sleep(10000);
            return info.FirstName + " " + info.LastName;
        }

    }
}
