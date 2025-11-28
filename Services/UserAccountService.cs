using IT_13FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IT_13FinalProject.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationDbContext _context;

        public UserAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                // Set default values
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;

                Console.WriteLine($"Adding user to context: {user.Username}");
                _context.Users.Add(user);
                Console.WriteLine($"Saving changes to database...");

                await _context.SaveChangesAsync();
                Console.WriteLine($"User saved successfully with ID: {user.UserId}");

                // Create role-specific profile (skip Patient - handled separately)
                if (user.Role?.ToLower() != "patient")
                {
                    await CreateRoleSpecificProfileAsync(user);
                }

                return user;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the detailed database error
                Console.WriteLine($"Database update error: {dbEx.Message}");
                Console.WriteLine($"Inner exception: {dbEx.InnerException?.Message}");

                // Log the SQL if available
                if (dbEx.InnerException != null)
                {
                    Console.WriteLine($"Inner exception details: {dbEx.InnerException}");
                }

                throw new Exception($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw new Exception($"Failed to create user: {ex.Message}");
            }
        }

        public async Task<bool> CreateRoleSpecificProfileAsync(User user)
        {
            try
            {
                switch (user.Role?.ToLower())
                {
                    case "doctor":
                        var doctor = new Doctor
                        {
                            UserId = user.UserId,
                            FirstName = user.FullName?.Split(' ').FirstOrDefault() ?? string.Empty,
                            LastName = user.FullName?.Split(' ').LastOrDefault() ?? string.Empty,
                            Email = user.Email
                        };
                        _context.Doctors.Add(doctor);
                        break;

                    case "nurse":
                        var nurse = new Nurse
                        {
                            UserId = user.UserId,
                            FirstName = user.FullName?.Split(' ').FirstOrDefault() ?? string.Empty,
                            LastName = user.FullName?.Split(' ').LastOrDefault() ?? string.Empty,
                            Email = user.Email
                        };
                        _context.Nurses.Add(nurse);
                        break;

                    case "billing staff":
                        var billingStaff = new BillingStaff
                        {
                            UserId = user.UserId,
                            FirstName = user.FullName?.Split(' ').FirstOrDefault() ?? string.Empty,
                            LastName = user.FullName?.Split(' ').LastOrDefault() ?? string.Empty,
                            Email = user.Email
                        };
                        _context.BillingStaff.Add(billingStaff);
                        break;

                    case "inventory staff":
                        var inventoryStaff = new InventoryStaff
                        {
                            UserId = user.UserId,
                            FirstName = user.FullName?.Split(' ').FirstOrDefault() ?? string.Empty,
                            LastName = user.FullName?.Split(' ').LastOrDefault() ?? string.Empty,
                            Email = user.Email
                        };
                        _context.InventoryStaff.Add(inventoryStaff);
                        break;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating role-specific profile: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                // Don't throw here - the user was created successfully, just the profile failed
                return false;
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user by email: {ex.Message}");
                return null;
            }
        }
    }
}