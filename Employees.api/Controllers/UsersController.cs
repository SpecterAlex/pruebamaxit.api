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
    public class UsersController : ControllerBase
    {
        readonly ILogger<UsersController> _logger;
        readonly IUsersRepository _usersRepository;
        readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUsersRepository usersRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(List<User>), 400)]
        public async Task<IActionResult> GetUsersList()
        {
            var response = new GenericResponse<List<User>>();
            try
            {
                var res = await _usersRepository.GetUsersList();
                response.Data = res;
                response.TotalRecords = res.Count;
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<List<User>>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<List<User>>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(User), 400)]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = new GenericResponse<User>();
            try
            {
                response.Data = await _usersRepository.GetUser(id);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<User>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<User>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        [NoCache]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(User), 400)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto model)
        {
            var response = new GenericResponse<User>();
            try
            {
                User userToCreate = _mapper.Map<User>(model);
                response.Data = await _usersRepository.CreateUser(userToCreate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<User>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<User>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPut]
        [NoCache]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(User), 400)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto model)
        {
            var response = new GenericResponse<User>();
            try
            {
                User userToUpdate = _mapper.Map<User>(model);
                response.Data = await _usersRepository.UpdateUser(userToUpdate);
                return Ok(response);
            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<User>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<User>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [NoCache]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                response.Data = await _usersRepository.DeleteUser(id);
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
