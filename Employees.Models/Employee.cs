using System;
using System.Collections.Generic;

namespace Employees.Models
{
    public class Employee
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
        public virtual Country Country { get; set; }
        public virtual List<Beneficiary> Beneficiaries { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
