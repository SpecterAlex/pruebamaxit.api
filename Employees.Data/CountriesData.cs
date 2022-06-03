using Dapper;
using Microsoft.Extensions.Configuration;
using Employees.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Employees.Data
{
    public class CountriesData : BaseRepo
    {
        public CountriesData(IConfiguration config) : base(config)
        {
        }
        public List<Country> GetCountriesList()
        {
            var storedProcedure = "dbo.spSelCountryList";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            var countries = r.Read<Country>();
            List<Country> countryList = countries.ToList();

            return countryList;
        }
    }
}
