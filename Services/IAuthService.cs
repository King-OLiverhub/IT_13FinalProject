using IT_13FinalProject.Models;
using System.Threading.Tasks;

namespace IT_13FinalProject.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string username, string password);
        Task InitializeDatabaseAsync();
        Task<bool> CreateDefaultAdminAsync();
    }
}