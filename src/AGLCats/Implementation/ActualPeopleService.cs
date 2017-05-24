using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AGLCats.Tests;
using Newtonsoft.Json;

namespace AGLCats.Implementation
{
    public class ActualPeopleService : IPeopleService
    {
        HttpClient httpClient;

        public ActualPeopleService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://agl-developer-test.azurewebsites.net")
            };
        }

        public async Task<IEnumerable<Person>> Get()
        {
            var response = await httpClient.GetAsync("people.json").ConfigureAwait(false);
            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<Person>>(jsonString);
        }
    }
}