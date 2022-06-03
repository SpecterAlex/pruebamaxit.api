using Employees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Business.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int userId);
        Task<List<User>> GetUsersList();
        Task<User> GetUser(int userId);
    }
}
