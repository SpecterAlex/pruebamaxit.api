using Employees.Business.Interfaces;
using Employees.Data;
using Employees.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Employees.Business.Repositories
{
    public class AuthRepository: IAuthRepository 
    {
        readonly ILogger<AuthRepository> _logger;
        readonly IPasswordHasher<User> _passwordHasher;
        readonly UsersData _usersData;
        public AuthRepository(ILogger<AuthRepository> logger, IPasswordHasher<User> passwordHasher, UsersData usersData)
        {
            _logger = logger;
            _passwordHasher = passwordHasher;
            _usersData = usersData;
        }

        public async Task<(bool isValid, User user)> SignIn(SingIn singIn)
        {
            try
            {
                var userdb = _usersData.GetUserByEmail(singIn.Email);
                if (userdb != null)
                {
                    var resultado = _passwordHasher.VerifyHashedPassword(userdb, userdb.Password, singIn.Password);

                    return (resultado == PasswordVerificationResult.Success, userdb);
                }
                else
                {
                    _logger.LogWarning($"User not found ${singIn.Email}");
                    return (false, new User());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error ${ex.Message}");
                return (false, new User());
            }
        }
    }
}
