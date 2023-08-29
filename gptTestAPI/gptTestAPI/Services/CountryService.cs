using gptTestAPI.Models;

namespace gptTestAPI.Services
{
    public class CountryService
    {
        public static List<Country> FilterCountriesByString(string? queryString, List<Country> countries)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return countries;
            }

            queryString = queryString.ToLower();

            var filteredCountries = countries.Where(country => country.Name.Common.ToLower().Contains(queryString)).ToList();

            return filteredCountries;
        }

        public static List<Country> FilterCountriesByPopulation(string maxPopulationInMillions, List<Country> countries)
        {
            if (maxPopulationInMillions == null)
            {
                return countries;
            }

            var filteredCountries = countries.Where(c => c.Population < float.Parse(maxPopulationInMillions) * 1000000).ToList();

            return filteredCountries;
        }

        public static List<Country> SortCountries(string? order, List<Country> countries)
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

        public static List<Country> GetFirstNRecords(int? n, List<Country> countries)
        {
            if (n != null && n > 0)
            {
                return countries.Take((int)n).ToList();
            }
            else
            {
                return countries;
            }
        }
    }
}
