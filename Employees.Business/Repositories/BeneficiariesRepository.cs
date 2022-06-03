using Employees.Business.Interfaces;
using Employees.Data;
using Employees.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Repositories
{
    public class BeneficiariesRepository : IBeneficiariesRepository
    {
        readonly ILogger<BeneficiariesRepository> _logger;
        readonly BeneficiariesData _beneficiariesData;

        public BeneficiariesRepository(ILogger<BeneficiariesRepository> logger, BeneficiariesData beneficiariesData)
        {
            _logger = logger;
            _beneficiariesData = beneficiariesData;
        }

        public async Task<bool> CreateBeneficiary(Beneficiary beneficiary) => _beneficiariesData.CreateBeneficiary(beneficiary);

        public async Task<bool> UpdateBeneficiary(Beneficiary beneficiary) => _beneficiariesData.UpdateBeneficiary(beneficiary);

        public async Task<bool> DeleteBeneficiary(int beneficiaryId) => _beneficiariesData.DeleteBeneficiary(beneficiaryId);

        public async Task<Beneficiary> GetBeneficiary(int beneficiaryId) => _beneficiariesData.GetBeneficiary(beneficiaryId);

        public async Task<List<Beneficiary>> GetBeneficiariesList() => _beneficiariesData.GetBeneficiariesList();

    }
}