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
    public class EmployeesController : ControllerBase
    {
        readonly ILogger<EmployeesController> _logger;
        readonly IEmployeesRepository _employeesRepository;
        readonly IMapper _mapper;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesRepository employeesRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(typeof(List<Employee>), 400)]
        public async Task<IActionResult> GetEmployeesList()
        {
            var response = new GenericResponse<List<Employee>>();
            try
            {
                var res = await _employeesRepository.GetEmployeesList();
                response.Data = res;
                response.TotalRecords = res.Count;
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<List<Employee>>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<List<Employee>>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(Employee), 400)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = new GenericResponse<Employee>();
            try
            {
                response.Data = await _employeesRepository.GetEmployee(id);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<Employee>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<Employee>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        [NoCache]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(Employee), 400)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto model)
        {
            var response = new GenericResponse<Employee>();
            try
            {
                Employee employeeToCreate = _mapper.Map<Employee>(model);
                response.Data = await _employeesRepository.CreateEmployee(employeeToCreate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<Employee>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<Employee>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPut]
        [NoCache]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(Employee), 400)]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto model)
        {
            var response = new GenericResponse<Employee>();
            try
            {
                Employee employeeToUpdate = _mapper.Map<Employee>(model);
                response.Data = await _employeesRepository.UpdateEmployee(employeeToUpdate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<Employee>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<Employee>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [NoCache]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                response.Data = await _employeesRepository.DeleteEmployee(id);
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
