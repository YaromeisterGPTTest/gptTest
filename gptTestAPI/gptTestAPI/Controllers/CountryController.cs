using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using gptTestAPI.Models;
using gptTestAPI.Services;
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

            response = CountryService.FilterCountriesByString(param1, response);
            response = CountryService.FilterCountriesByPopulation(param2, response);
            response = CountryService.SortCountries(param3, response);
            response = CountryService.GetFirstNRecords(param4, response);

            return Ok(response);
        }


    }
}
