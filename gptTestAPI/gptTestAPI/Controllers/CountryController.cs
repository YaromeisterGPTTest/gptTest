using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private const string ApiUrl = "https://restcountries.com/v3.1/all";

    [HttpGet]
    public async Task<IActionResult> GetCountries(string param1 = null, int? param2 = null, string param3 = null, int? param4 = null)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(ApiUrl);

        // Do something with the parameters and countries data if needed

        return Ok(response);
    }
}
