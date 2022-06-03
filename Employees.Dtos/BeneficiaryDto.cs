using System;

namespace Employees.Dtos
{
    public class BeneficiaryDto
    {
        public int? BeneficiaryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int EmployeeId { get; set; }
        public decimal ParticipationPercent { get; set; }
        public virtual bool? Deleted { get; set; }
    }
}
