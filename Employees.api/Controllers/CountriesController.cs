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
    public class CountriesController : ControllerBase
    {
        readonly ILogger<CountriesController> _logger;
        readonly ICountriesRepository _countriesRepository;
        readonly IMapper _mapper;

        public CountriesController(ILogger<CountriesController> logger, ICountriesRepository countriesRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _countriesRepository = countriesRepository ?? throw new ArgumentNullException(nameof(countriesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Country>), 200)]
        [ProducesResponseType(typeof(List<Country>), 400)]
        public async Task<IActionResult> GetCountriesList()
        {
            var response = new GenericResponse<List<Country>>();
            try
            {
                var res = await _countriesRepository.GetCountriesList();
                response.Data = res;
                response.TotalRecords = res.Count;
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<List<Country>>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<List<Country>>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

    }
}
