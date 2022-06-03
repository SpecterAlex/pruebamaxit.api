using AutoMapper;
using Employees.Api.Services;
using Employees.Business.Interfaces;
using Employees.Dtos;
using Employees.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository, TokenService tokenService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("signin")]
        [HttpPost]
        [NoCache]
        public async Task<ActionResult> SignIn(SingInRequestDto singInRequestDto)
        {
            var response = new GenericResponse<SingInResponse>();
            try
            {
                SingInResponse singInResponse = new();

                var singIn = _mapper.Map<SingIn>(singInRequestDto);
                var (isValid, user) = await _authRepository.SignIn(singIn);

                if (!isValid)
                {
                    return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Invalid username or password" });
                }

                singInResponse.User = user;
                singInResponse.AccessToken = await _tokenService.TokenGenerate(user); ;

                response.Data = singInResponse;
                return Ok(response);


            }
            catch (SqlException sqlEx)
            {
                response = new GenericResponse<SingInResponse>(sqlEx);
                _logger.LogError(sqlEx.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new GenericResponse<SingInResponse>(ex);
                _logger.LogError(ex.Message);
                return BadRequest(response);
            }
        }

    }
}
