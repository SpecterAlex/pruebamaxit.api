using Employees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Interfaces
{
    public interface ICountriesRepository
    {
        Task<List<Country>> GetCountriesList();
    }
}
