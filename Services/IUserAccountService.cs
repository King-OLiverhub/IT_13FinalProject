using IT_13FinalProject.Models;
using System.Threading.Tasks;

namespace IT_13FinalProject.Services
{
    public interface IUserAccountService
    {
        Task<bool> UserExistsAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<bool> CreateRoleSpecificProfileAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}