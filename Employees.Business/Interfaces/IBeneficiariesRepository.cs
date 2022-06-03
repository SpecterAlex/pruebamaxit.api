using Employees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Interfaces
{
    public interface IBeneficiariesRepository
    {
        Task<bool> CreateBeneficiary(Beneficiary employee);
        Task<bool> UpdateBeneficiary(Beneficiary employee);
        Task<bool> DeleteBeneficiary(int employeeId);
        Task<List<Beneficiary>> GetBeneficiariesList();
        Task<Beneficiary> GetBeneficiary(int employeeId);
    }
}
