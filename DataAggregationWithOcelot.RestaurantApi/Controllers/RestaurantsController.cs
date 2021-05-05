using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAggregationWithOcelot.RestaurantApi.Controllers
{
    [ApiController]
    [Route("restaurants")]
    public class RestaurantsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Restaurant
            {
                Id = Guid.NewGuid(),
                Address = "Amsterdam",
                Name = $"Restaurant#{index}"
            })
            .ToArray();
        }
    }
}
