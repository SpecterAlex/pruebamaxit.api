using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Dtos
{
    public class EmployeeDto
    {
        public int? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmployeeNumber { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public List<BeneficiaryDto> Beneficiaries { get; set; }
    }
}
