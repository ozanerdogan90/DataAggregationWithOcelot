using System;

namespace DataAggregationWithOcelot.RestaurantApi
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
