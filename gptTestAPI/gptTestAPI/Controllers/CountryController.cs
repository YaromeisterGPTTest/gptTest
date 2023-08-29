using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using gptTestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private const string ApiUrl = "https://restcountries.com/v3.1/all";

        [HttpGet]
        public async Task<IActionResult> GetCountries(string? param1 = null, int? param2 = null, string? param3 = null, int? param4 = null)
        {
            using var httpClient = new HttpClient();
            var response = JsonSerializer.Deserialize<List<Country>>(await httpClient.GetStringAsync(ApiUrl), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            response = FilterCountriesByString(param1, response);
            response = FilterCountriesByPopulation(param2, response);
            response = SortCountries(param3, response);
            response = GetFirstNRecords(param4, response);

            return Ok(response);
        }

        private List<Country> FilterCountriesByString(string? queryString, List<Country> countries)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return countries;
            }

            queryString = queryString.ToLower();

            var filteredCountries = countries.Where(country => country.Name.Common.ToLower().Contains(queryString)).ToList();

            return filteredCountries;
        }

        private List<Country> FilterCountriesByPopulation(int? maxPopulationInMillions, List<Country> countries)
        {
            if (maxPopulationInMillions == null)
            {
                return countries;
            }

            var filteredCountries = countries.Where(c => c.Population < maxPopulationInMillions * 1000000).ToList();

            return filteredCountries;
        }

        private List<Country> SortCountries(string? order, List<Country> countries)
        {
            if (order?.ToLower() != "ascend" && order?.ToLower() != "descend")
            {
                return countries;
            }

            if (order.ToLower() == "descend")
            {
                countries = countries.OrderByDescending(c => c.Name.Common.ToLower()).ToList();
            }
            else
            {
                countries = countries.OrderBy(c => c.Name.Common.ToLower()).ToList();
            }

            return countries;
        }

        private List<Country> GetFirstNRecords(int? n, List<Country> countries)
        {
            if (n != null && n > 0)
            {
                return countries.Take((int)n).ToList();
            } else
            {
                return countries;
            }
        }
    }
}
