using Dapper;
using Microsoft.Extensions.Configuration;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Data
{
    public class EmployeesData : BaseRepo
    {
        readonly BeneficiariesData _beneficiariesData;
        public EmployeesData(IConfiguration config, BeneficiariesData beneficiariesData) : base(config)
        {
            _beneficiariesData = beneficiariesData;
        }
        public Employee CreateEmployee(Employee employee)
        {
            var response = new Employee();
            var storedProcedure = "dbo.spInsEmployee";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@FirstName", employee.FirstName);
            parameters.Add("@LastName", employee.LastName);
            parameters.Add("@BirthDate", employee.BirthDate);
            parameters.Add("@EmployeeNumber", employee.EmployeeNumber);
            parameters.Add("@Curp", employee.Curp);
            parameters.Add("@Ssn", employee.Ssn);
            parameters.Add("@PhoneNumber", employee.PhoneNumber);
            parameters.Add("@CountryId", employee.CountryId);
            response = connection.Query<Employee>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            if (response != null)
            {
                if (employee.Beneficiaries.Count > 0)
                {
                    int employeeId = (int)response.EmployeeId;
                    response.Beneficiaries = new();
                    foreach (var beneficiary in employee.Beneficiaries)
                    {
                        beneficiary.EmployeeId = employeeId;
                        var resultBeneficiary = _beneficiariesData.CreateBeneficiary(beneficiary);
                        //if (resultBeneficiary)
                        //else
                        //log, insert o lo que se hara para guardar fallos
                    }

                }
                return response;
            }
            else
                throw new Exception("Error al insertar");
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var storedProcedure = "dbo.spUpdEmployee";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@EmployeeId", employee.EmployeeId);
            parameters.Add("@FirstName", employee.FirstName);
            parameters.Add("@LastName", employee.LastName);
            parameters.Add("@BirthDate", employee.BirthDate);
            parameters.Add("@EmployeeNumber", employee.EmployeeNumber);
            parameters.Add("@Curp", employee.Curp);
            parameters.Add("@Ssn", employee.Ssn);
            parameters.Add("@PhoneNumber", employee.PhoneNumber);
            parameters.Add("@CountryId", employee.CountryId);
            var result = connection.Query<Employee>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            if (result != null)
            {
                var response = new GenericResponse<bool>();
                int cont = 0;
                foreach (var ben in employee.Beneficiaries)
                {
                    ben.EmployeeId = (int)employee.EmployeeId;

                    var res = false;
                    if (ben.BeneficiaryId != null)
                    {
                        if (ben.Deleted == true)
                        {
                            res = _beneficiariesData.DeleteBeneficiary((int)ben.BeneficiaryId);
                        }
                        else
                        {
                            res = _beneficiariesData.UpdateBeneficiary(ben);
                        }
                    }
                    else
                    {
                        res = _beneficiariesData.CreateBeneficiary(ben);
                    }

                    if (res)
                        cont++;
                }
                //validacion o mensaje con el contador de registros afectados
            }
            return result;
        }

        public bool DeleteEmployee(int employeeId)
        {
            var storedProcedure = "dbo.spDelEmployee";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@EmployeeId", employeeId);
            var r = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return r == 1;
        }

        public Employee GetEmployee(int employeeId)
        {
            var storedProcedure = "dbo.spSelEmployee";
            Employee employee = new();
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@EmployeeId", employeeId);
            var result = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            employee = result.Read<Employee>().FirstOrDefault();
            var beneficiaries = result.Read<Beneficiary>();
            employee.Beneficiaries = beneficiaries.Where(z => z.EmployeeId == employee.EmployeeId).ToList();

            return employee;
        }

        public List<Employee> GetEmployeesList()
        {
            var employeeList = new List<Employee>();

            var storedProcedure = "dbo.spSelEmployeeList";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            var employees = r.Read<Employee>();
            var countries = r.Read<Country>();

            foreach (var employee in employees)
            {
                employee.Country = countries.FirstOrDefault(w => w.CountryId == employee.CountryId);
            }
            employeeList = employees.ToList();

            return employeeList;
        }
    }
}
