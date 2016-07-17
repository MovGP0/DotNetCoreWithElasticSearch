using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace Backend.Locations
{
    [Route("location")]
    public sealed class LocationController : Controller
    {
        [HttpGet("{id}")]
        public async Task<Location> Get(int id, CancellationToken cancellationToken)
        {
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync($"program/location/{id}/_source", cancellationToken);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<Location>(responseBody);
                return location;
            }
        }

        [HttpGet("")]
        public async Task<IEnumerable<Location>> Get(CancellationToken cancellationToken)
        {
/* TODO:
GET program/location/_search
{
   "query": {
      "match_all": {}
   }
}
*/
            return await Task.FromResult(new [] { 
                new Location{ Name="Foo" }, 
                new Location{ Name="Bar"} 
            });
        }

        [HttpPost("{id}")]
        public async Task Post(int id, [FromBody] Location location, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(location);
            using(var client = new HttpClient())
            {
                var response = await client.PutAsync($"http://localhost:9200/program/location/{id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"), cancellationToken);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}