using System;

namespace DataAggregationWithOcelot.ReviewsApi
{
    public class Review
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }

    }
}
