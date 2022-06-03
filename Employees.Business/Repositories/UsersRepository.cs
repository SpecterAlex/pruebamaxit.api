using Employees.Business.Interfaces;
using Employees.Data;
using Employees.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        readonly ILogger<UsersRepository> _logger;
        readonly UsersData _usersData;

        public UsersRepository(ILogger<UsersRepository> logger, UsersData usersData)
        {
            _logger = logger;
            _usersData = usersData;
        }

        public async Task<User> CreateUser(User user) => _usersData.CreateUser(user);

        public async Task<User> UpdateUser(User user) => _usersData.UpdateUser(user);

        public async Task<bool> DeleteUser(int userId) => _usersData.DeleteUser(userId);

        public async Task<User> GetUser(int userId) => _usersData.GetUser(userId);

        public async Task<List<User>> GetUsersList() => _usersData.GetUsersList();

    }
}