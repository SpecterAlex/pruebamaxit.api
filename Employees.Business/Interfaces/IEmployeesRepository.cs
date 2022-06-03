using Employees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int employeeId);
        Task<List<Employee>> GetEmployeesList();
        Task<Employee> GetEmployee(int employeeId);
    }
}
