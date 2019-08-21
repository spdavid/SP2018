using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NETCoreTypeScriptPnPJsMSAL.Controllers
{
    [Route("api/[controller]")]
    public class GetCarsController : Controller
    {
 
        [HttpGet("[action]")]
        public IEnumerable<Car> getCars()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Car
            {
                Id = index,
                Name = "Volvo",
                Color = "Black"
            });
        }

        public class Car
        {
                public int Id { get; set; }
                public string Name { get; set; }

                public string Color { get; set; }



        }
    }
}
