﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using gptTestAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private const string ApiUrl = "https://restcountries.com/v3.1/all";

    [HttpGet]
    public async Task<IActionResult> GetCountries(string? param1 = null, int? param2 = null, string? param3 = null, int? param4 = null)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(ApiUrl);

        response = FilterCountriesByString(param1, response);

        return Ok(response);
    }

    private string FilterCountriesByString(string queryString, string jsonCountries)
    {
        if (string.IsNullOrEmpty(queryString))
        {
            return jsonCountries;
        }
        List<Country> countries = JsonSerializer.Deserialize<List<Country>>(jsonCountries, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        queryString = queryString.ToLower();

        var filteredCountries = countries.Where(country => country.Name.Common.ToLower().Contains(queryString)).ToList();

        return JsonSerializer.Serialize(filteredCountries);
    }
}
