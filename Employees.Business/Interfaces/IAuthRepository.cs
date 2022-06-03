using Employees.Models;
using System.Threading.Tasks;

namespace Employees.Business.Interfaces
{
    public interface IAuthRepository
    {
        Task<(bool isValid, User user)> SignIn(SingIn singIn);
    }
}
