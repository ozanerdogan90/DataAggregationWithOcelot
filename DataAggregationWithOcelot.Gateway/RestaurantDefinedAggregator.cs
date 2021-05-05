using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Ocelot.Configuration;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gateway.Api
{
    internal class RestaurantDefinedAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var (restaurantsResponse, restaurantResponseStatusCode) = await ReadContext(responses, "restaurants");
            var (reviewsResponse, reviewsResponseStatusCode) = await ReadContext(responses, "reviews");

            if (!IsSuccessStatusCode(restaurantResponseStatusCode))
            {
                return new DownstreamResponse(null, restaurantResponseStatusCode, new List<KeyValuePair<string, IEnumerable<string>>>(), "Error");
            }

            var result = JArray.Parse(restaurantsResponse);
            var reviews = (!string.IsNullOrEmpty(reviewsResponse) && IsSuccessStatusCode(reviewsResponseStatusCode)) ? JArray.Parse(reviewsResponse).Select(y => JObject.Parse(y.ToString())).ToList() : null;

            foreach (JObject restaurant in result)
            {
                var restaurantReviews = reviews?.Where(y => y["restaurantName"].ToString().Equals(restaurant["name"].ToString(), System.StringComparison.InvariantCultureIgnoreCase));

                if (restaurantReviews != null && restaurantReviews.Any())
                    restaurant.Add("reviews", JArray.FromObject(restaurantReviews));
            }

            var stringContent = new StringContent(result.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }

        private async Task<(string, HttpStatusCode)> ReadContext(List<HttpContext> responses, string key)
        {
            var response = responses.FirstOrDefault(y => ((DownstreamRoute)y.Items["DownstreamRoute"]).Key == key);
            var downstreamResponse = ((DownstreamResponse)response.Items["DownstreamResponse"]);
            var responseString = await downstreamResponse.Content.ReadAsStringAsync();
            return (responseString, downstreamResponse.StatusCode);
        }

        public bool IsSuccessStatusCode(HttpStatusCode httpStatusCode)
        {
            return ((int)httpStatusCode >= 200) && ((int)httpStatusCode <= 299);
        }
    }
}
