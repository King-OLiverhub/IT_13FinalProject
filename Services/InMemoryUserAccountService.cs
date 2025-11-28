// Services/InMemoryUserAccountService.cs
using IT_13FinalProject.Models;
using System.Collections.Concurrent;

namespace IT_13FinalProject.Services
{
    public class InMemoryUserAccountService : IUserAccountService
    {
        private readonly ConcurrentDictionary<string, User> _users = new();
        private int _nextId = 1;

        public InMemoryUserAccountService()
        {
            // Add default users
            AddDefaultUsers();
        }

        private void AddDefaultUsers()
        {
            var defaultUsers = new[]
            {
                new User { UserId = _nextId++, Username = "admin", PasswordHash = "admin123", Role = "Admin", Email = "admin@medflow.com", FullName = "System Administrator", CreatedAt = DateTime.Now, IsActive = true },
                new User { UserId = _nextId++, Username = "doctor", PasswordHash = "12345", Role = "Doctor", Email = "doctor@medflow.com", FullName = "Doctor User", CreatedAt = DateTime.Now, IsActive = true },
                new User { UserId = _nextId++, Username = "nurse", PasswordHash = "12345", Role = "Nurse", Email = "nurse@medflow.com", FullName = "Nurse User", CreatedAt = DateTime.Now, IsActive = true },
                new User { UserId = _nextId++, Username = "billing", PasswordHash = "12345", Role = "Billing Staff", Email = "billing@medflow.com", FullName = "Billing Staff", CreatedAt = DateTime.Now, IsActive = true },
                new User { UserId = _nextId++, Username = "inventory", PasswordHash = "12345", Role = "Inventory Staff", Email = "inventory@medflow.com", FullName = "Inventory Staff", CreatedAt = DateTime.Now, IsActive = true }
            };

            foreach (var user in defaultUsers)
            {
                _users[user.Username.ToLower()] = user;
            }
        }

        public Task<bool> UserExistsAsync(string username)
        {
            return Task.FromResult(_users.ContainsKey(username.ToLower()));
        }

        public Task<User> CreateUserAsync(User user)
        {
            user.UserId = _nextId++;
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;
            _users[user.Username.ToLower()] = user;
            return Task.FromResult(user);
        }

        public Task<bool> CreateRoleSpecificProfileAsync(User user)
        {
            // For in-memory service, we don't create role-specific profiles
            return Task.FromResult(true);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            var user = _users.Values.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        // Helper method to get all users (for debugging)
        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.Values.ToList());
        }
    }
}