using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAggregationWithOcelot.ReviewsApi.Controllers
{
    [ApiController]
    [Route("reviews")]
    public class ReviewsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Review> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 50).Select(index => new Review
            {
                Id = Guid.NewGuid(),
                RestaurantName = $"Restaurant#{rng.Next(1, 5)}",
                Star= rng.Next(1, 5)
            })
            .ToArray();
        }
    }
}
