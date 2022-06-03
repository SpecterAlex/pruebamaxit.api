using AutoMapper;
using Employees.Business.Interfaces;
using Employees.Dtos;
using Employees.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Employees.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        readonly ILogger<BeneficiariesController> _logger;
        readonly IBeneficiariesRepository _beneficiariesRepository;
        readonly IMapper _mapper;

        public BeneficiariesController(ILogger<BeneficiariesController> logger, IBeneficiariesRepository beneficiariesRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _beneficiariesRepository = beneficiariesRepository ?? throw new ArgumentNullException(nameof(beneficiariesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Beneficiary>), 200)]
        [ProducesResponseType(typeof(List<Beneficiary>), 400)]
        public async Task<IActionResult> GetBeneficiariesList()
        {
            var response = new GenericResponse<List<Beneficiary>>();
            try
            {
                var res = await _beneficiariesRepository.GetBeneficiariesList();
                response.Data = res;
                response.TotalRecords = res.Count;
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<List<Beneficiary>>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<List<Beneficiary>>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(Beneficiary), 200)]
        [ProducesResponseType(typeof(Beneficiary), 400)]
        public async Task<IActionResult> GetBeneficiary(int beneficiaryid)
        {
            var response = new GenericResponse<Beneficiary>();
            try
            {
                response.Data = await _beneficiariesRepository.GetBeneficiary(beneficiaryid);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<Beneficiary>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<Beneficiary>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        [NoCache]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> CreateBeneficiary([FromBody] BeneficiaryDto model)
        {
            var response = new GenericResponse<bool>();
            try
            {
                Beneficiary beneficiaryToCreate = _mapper.Map<Beneficiary>(model);
                response.Data = await _beneficiariesRepository.CreateBeneficiary(beneficiaryToCreate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<bool>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<bool>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPut]
        [NoCache]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> UpdateBeneficiary([FromBody] BeneficiaryDto model)
        {
            var response = new GenericResponse<bool>();
            try
            {
                Beneficiary beneficiaryToUpdate = _mapper.Map<Beneficiary>(model);
                response.Data = await _beneficiariesRepository.UpdateBeneficiary(beneficiaryToUpdate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<bool>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<bool>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [NoCache]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> DeleteBeneficiary(int beneficiaryid)
        {
            var response = new GenericResponse<bool>();
            try
            {
                response.Data = await _beneficiariesRepository.DeleteBeneficiary(beneficiaryid);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<bool>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<bool>(ex)
                {
                    Data = false
                };
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }
    }
}
