using IT_13FinalProject.Models;
using System.Collections.Concurrent;

namespace IT_13FinalProject.Services
{
    public class InMemoryAuthService : IAuthService
    {
        private readonly IUserAccountService _userAccountService;

        public InMemoryAuthService(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public Task<User?> ValidateUserAsync(string username, string password)
        {
            // For in-memory service, we'll use a simple approach
            // In a real scenario, you'd hash the password
            var users = new Dictionary<string, (string password, User user)>
            {
                { "admin", ("admin123", new User { Username = "admin", Role = "Admin", Email = "admin@medflow.com", FullName = "System Administrator" }) },
                { "doctor", ("12345", new User { Username = "doctor", Role = "Doctor", Email = "doctor@medflow.com", FullName = "Doctor User" }) },
                { "nurse", ("12345", new User { Username = "nurse", Role = "Nurse", Email = "nurse@medflow.com", FullName = "Nurse User" }) },
                { "billing", ("12345", new User { Username = "billing", Role = "Billing Staff", Email = "billing@medflow.com", FullName = "Billing Staff" }) },
                { "inventory", ("12345", new User { Username = "inventory", Role = "Inventory Staff", Email = "inventory@medflow.com", FullName = "Inventory Staff" }) }
            };

            if (users.TryGetValue(username.ToLower(), out var userInfo) && userInfo.password == password)
            {
                return Task.FromResult<User?>(userInfo.user);
            }

            return Task.FromResult<User?>(null);
        }

        public Task InitializeDatabaseAsync()
        {
            // Nothing to initialize for in-memory
            return Task.CompletedTask;
        }

        public Task<bool> CreateDefaultAdminAsync()
        {
            // Default admin already exists in in-memory service
            return Task.FromResult(true);
        }
    }
}