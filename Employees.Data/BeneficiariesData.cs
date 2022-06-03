using Dapper;
using Microsoft.Extensions.Configuration;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Employees.Data
{
    public class BeneficiariesData : BaseRepo
    {
        public BeneficiariesData(IConfiguration config) : base(config)
        {
        }
        public bool CreateBeneficiary(Beneficiary beneficiary)
        {
            var response = false;

            var storedProcedure = "dbo.spInsBeneficiary";
            try
            {
                using var connection = GetOpenConnection();
                DynamicParameters parameters = new();
                parameters.Add("@FirstName", beneficiary.FirstName);
                parameters.Add("@LastName", beneficiary.LastName);
                parameters.Add("@BirthDate", beneficiary.BirthDate);
                parameters.Add("@Curp", beneficiary.Curp);
                parameters.Add("@Ssn", beneficiary.Ssn);
                parameters.Add("@PhoneNumber", beneficiary.PhoneNumber);
                parameters.Add("@CountryId", beneficiary.CountryId);
                parameters.Add("@EmployeeId", beneficiary.EmployeeId);
                parameters.Add("@ParticipationPercent", beneficiary.ParticipationPercent);
                var r = connection.Query<Beneficiary>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (r != null)
                    response = true;
                return response;
            }
            catch
            {
                response = false;
                return response;
            }
            
        }

        public bool UpdateBeneficiary(Beneficiary beneficiary)
        {
            var response = false;

            var storedProcedure = "dbo.spUpdBeneficiary";
            try
            {
                using var connection = GetOpenConnection();
                DynamicParameters parameters = new();
                parameters.Add("@BeneficiaryId", beneficiary.BeneficiaryId);
                parameters.Add("@FirstName", beneficiary.FirstName);
                parameters.Add("@LastName", beneficiary.LastName);
                parameters.Add("@BirthDate", beneficiary.BirthDate);
                parameters.Add("@Curp", beneficiary.Curp);
                parameters.Add("@Ssn", beneficiary.Ssn);
                parameters.Add("@PhoneNumber", beneficiary.PhoneNumber);
                parameters.Add("@CountryId", beneficiary.CountryId);
                parameters.Add("@EmployeeId", beneficiary.EmployeeId);
                parameters.Add("@ParticipationPercent", beneficiary.ParticipationPercent);
                var r = connection.Query<Beneficiary>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (r != null)
                    response = true;
                return response;
            }
            catch
            {
                response = false;
                return response;
            }
            
        }

        public bool DeleteBeneficiary(int beneficiaryId)
        {
            var storedProcedure = "dbo.spDelBeneficiary";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@BeneficiaryId", beneficiaryId);
            var r = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return r == 1;
        }

        public Beneficiary GetBeneficiary(int beneficiaryId)
        {
            var storedProcedure = "dbo.spSelBeneficiary";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@BeneficiaryId", beneficiaryId);
            Beneficiary beneficiary = connection.Query<Beneficiary>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return beneficiary;
        }

        public List<Beneficiary> GetBeneficiariesList()
        {
            var beneficiariesList = new List<Beneficiary>();
           
            var storedProcedure = "dbo.spSelBeneficiariesList";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            var beneficiaries = r.Read<Beneficiary>();
            var countries = r.Read<Country>();

            foreach (var beneficiary in beneficiaries)
            {
                beneficiary.Country = countries.FirstOrDefault(w => w.CountryId == beneficiary.CountryId);
            }
            beneficiariesList = beneficiaries.ToList();

            return beneficiariesList;
        }
    }
}
