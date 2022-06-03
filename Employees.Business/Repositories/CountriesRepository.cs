using Employees.Business.Interfaces;
using Employees.Data;
using Employees.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        readonly ILogger<CountriesRepository> _logger;
        readonly CountriesData _countriesData;

        public CountriesRepository(ILogger<CountriesRepository> logger, CountriesData countriesData)
        {
            _logger = logger;
            _countriesData = countriesData;
        }

        public async Task<List<Country>> GetCountriesList() => _countriesData.GetCountriesList();

    }
}