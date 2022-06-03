using Employees.Business.Interfaces;
using Employees.Data;
using Employees.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        readonly ILogger<EmployeesRepository> _logger;
        readonly EmployeesData _employeesData;

        public EmployeesRepository(ILogger<EmployeesRepository> logger, EmployeesData employeesData)
        {
            _logger = logger;
            _employeesData = employeesData;
        }

        public async Task<Employee> CreateEmployee(Employee employee) => _employeesData.CreateEmployee(employee);

        public async Task<Employee> UpdateEmployee(Employee employee) => _employeesData.UpdateEmployee(employee);

        public async Task<bool> DeleteEmployee(int employeeId) => _employeesData.DeleteEmployee(employeeId);

        public async Task<Employee> GetEmployee(int employeeId) => _employeesData.GetEmployee(employeeId);

        public async Task<List<Employee>> GetEmployeesList() => _employeesData.GetEmployeesList();

    }
}