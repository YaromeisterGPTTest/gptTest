using gptTestAPI.Models;
using gptTestAPI.Services;

namespace TestProject1
{
    [TestFixture]
    public class CountryServiceTests
    {
        private List<Country> _sampleCountries;

        [SetUp]
        public void SetUp()
        {
            _sampleCountries = new List<Country>
        {
            new Country { Name = new Name { Common = "Zambia" }, Population = 18000000 },
            new Country { Name = new Name { Common = "Angola" }, Population = 32000000 },
            new Country { Name = new Name { Common = "Brazil" }, Population = 210000000 }
        };
        }

        [Test]
        public void TestFilterCountriesByString()
        {
            var result = CountryService.FilterCountriesByString("br", _sampleCountries);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Brazil", result[0].Name.Common);
        }

        [Test]
        public void TestFilterCountriesByPopulation()
        {
            var result = CountryService.FilterCountriesByPopulation(25, _sampleCountries);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Zambia", result[0].Name.Common);
        }

        [Test]
        public void TestSortCountriesAscend()
        {
            var result = CountryService.SortCountries("ascend", _sampleCountries);
            Assert.AreEqual("Angola", result[0].Name.Common);
            Assert.AreEqual("Brazil", result[1].Name.Common);
            Assert.AreEqual("Zambia", result[2].Name.Common);
        }

        [Test]
        public void TestSortCountriesDescend()
        {
            var result = CountryService.SortCountries("descend", _sampleCountries);
            Assert.AreEqual("Zambia", result[0].Name.Common);
            Assert.AreEqual("Brazil", result[1].Name.Common);
            Assert.AreEqual("Angola", result[2].Name.Common);
        }

        [Test]
        public void TestGetFirstNRecords()
        {
            var result = CountryService.GetFirstNRecords(2, _sampleCountries);
            Assert.AreEqual(2, result.Count);
        }
    }
}