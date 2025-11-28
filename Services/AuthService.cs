using IT_13FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT_13FinalProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            try
            {
                // Test connection first
                var canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    return await ValidateHardcodedCredentialsAsync(username, password);
                }

                // Try database authentication - get user by username first
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.IsActive == true);

                if (user != null)
                {
                    // Verify the password using PasswordHasher
                    if (_passwordHasher.VerifyPassword(password, user.PasswordHash))
                    {
                        return user;
                    }
                }

                // Fallback to hardcoded credentials
                return await ValidateHardcodedCredentialsAsync(username, password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database authentication error: {ex.Message}");
                return await ValidateHardcodedCredentialsAsync(username, password);
            }
        }

        // Instance async helper — keeps signature stable for hot-reload
        // Marked static because it doesn't use instance state.
        private static async Task<User?> ValidateHardcodedCredentialsAsync(string username, string password)
        {
            var hardcodedUsers = new List<User>
            {
                new() { Username = "admin", PasswordHash = "admin123", Role = "Admin", Email = "admin@gmail.com", FullName = "System Admin" },
                new() { Username = "nurse", PasswordHash = "12345", Role = "Nurse", Email = "nurse@gmail.com", FullName = "Nurse User" },
                new() { Username = "doctor", PasswordHash = "12345", Role = "Doctor", Email = "doctor@gmail.com", FullName = "Doctor User" },
                new() { Username = "billing", PasswordHash = "12345", Role = "Billing Staff", Email = "billing@gmail.com", FullName = "Billing Staff" },
                new() { Username = "inventory", PasswordHash = "12345", Role = "Inventory Staff", Email = "inventory@gmail.com", FullName = "Inventory Staff" }
            };

            var found = hardcodedUsers.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.PasswordHash == password); // Note: Hardcoded users use plain text

            // keep the method async-friendly for hot-reload stability
            await Task.CompletedTask;
            return found;
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    throw new Exception("Cannot connect to database. Please check SQL Server connection.");
                }

                // Check if database needs to be created (for new databases)
                await _context.Database.EnsureCreatedAsync();

                // Create default admin if it doesn't exist
                await CreateDefaultAdminAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                throw new Exception($"Database connection failed: {ex.Message}");
            }
        }

        public async Task<bool> CreateDefaultAdminAsync()
        {
            try
            {
                var existingAdmin = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == "admin");

                if (existingAdmin == null)
                {
                    var adminUser = new User
                    {
                        Username = "admin",
                        PasswordHash = _passwordHasher.HashPassword("admin123"), // Hash the password
                        Role = "Admin",
                        Email = "admin@medflow.com",
                        FullName = "System Administrator",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };

                    _context.Users.Add(adminUser);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating default admin: {ex.Message}");
                return false;
            }
        }
    }
}