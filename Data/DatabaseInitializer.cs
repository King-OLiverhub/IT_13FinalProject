using IT_13FinalProject.Models;
using IT_13FinalProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace IT_13FinalProject.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, IConfiguration configuration)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Create patient_bill table if it doesn't exist
            await EnsurePatientBillTableExists(context, configuration);

            // Update database schema for inventory table
            try
            {
                var updateService = new DatabaseUpdateService(context, configuration);
                await updateService.EnsureInventoryColumnsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database schema update failed: {ex.Message}");
                // Continue with initialization even if schema update fails
            }

            // Check if there are any users
            if (!await context.Users.AnyAsync())
            {
                // Seed initial users
                var users = new List<User>
                {
                    new User
                    {
                        Username = "Admin",
                        Email = "admin@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                        Role = "Admin",
                        FullName = "System Administrator",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new User
                    {
                        Username = "Doctor",
                        Email = "doctor@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                        Role = "Doctor",
                        FullName = "John Doctor",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new User
                    {
                        Username = "Nurse",
                        Email = "nurse@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                        Role = "Nurse",
                        FullName = "Jane Nurse",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new User
                    {
                        Username = "Billing",
                        Email = "billing@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                        Role = "Billing Staff",
                        FullName = "Bob Billing",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new User
                    {
                        Username = "Inventory",
                        Email = "inventory@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("12345"),
                        Role = "Inventory Staff",
                        FullName = "Alice Inventory",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }

        private static async Task EnsurePatientBillTableExists(ApplicationDbContext context, IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='patient_bill' AND xtype='U')
                    CREATE TABLE patient_bill (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        patient_id INT NULL,
                        patient_name NVARCHAR(200) NOT NULL,
                        patient_email NVARCHAR(100) NULL,
                        assessment_status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
                        date_of_visit DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        total_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                        insurance_coverage DECIMAL(18,2) NOT NULL DEFAULT 0,
                        patient_responsibility DECIMAL(18,2) NOT NULL DEFAULT 0,
                        payment_status NVARCHAR(50) NOT NULL DEFAULT 'Unpaid',
                        payment_method NVARCHAR(50) NULL,
                        payment_date DATETIME2 NULL,
                        insurance_provider NVARCHAR(100) NULL,
                        created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        updated_at DATETIME2 NULL,
                        is_archived BIT NOT NULL DEFAULT 0,
                        archived_at DATETIME2 NULL,
                        CONSTRAINT FK_patient_bill_patients FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE SET NULL
                    )";

                using var command = new SqlCommand(createTableQuery, connection);
                await command.ExecuteNonQueryAsync();

                // Seed sample data if table is empty
                await SeedSamplePatientBills(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating patient_bill table: {ex.Message}");
            }
        }

        private static async Task SeedSamplePatientBills(ApplicationDbContext context)
        {
            try
            {
                if (!await context.PatientBills.AnyAsync())
                {
                    var sampleBills = new List<PatientBill>
                    {
                        new PatientBill
                        {
                            PatientName = "Himeko Murata",
                            PatientEmail = "himeko_murata@gmail.com",
                            AssessmentStatus = "For Discharge",
                            DateOfVisit = DateTime.Now.AddDays(-5),
                            TotalAmount = 1500.00m,
                            PaymentStatus = "Pending"
                        },
                        new PatientBill
                        {
                            PatientName = "Yae Miko",
                            PatientEmail = "miko.yae@mail.com",
                            AssessmentStatus = "For Discharge",
                            DateOfVisit = DateTime.Now.AddDays(-3),
                            TotalAmount = 2300.50m,
                            PaymentStatus = "Paid",
                            PaymentDate = DateTime.Now.AddDays(-2)
                        },
                        new PatientBill
                        {
                            PatientName = "Black Swan",
                            PatientEmail = "ms.blkswan@mail.com",
                            AssessmentStatus = "For Discharge",
                            DateOfVisit = DateTime.Now.AddDays(-1),
                            TotalAmount = 890.75m,
                            PaymentStatus = "Pending"
                        },
                        new PatientBill
                        {
                            PatientName = "Ruan Mei",
                            PatientEmail = "therm_ei@gmail.com",
                            AssessmentStatus = "For Discharge",
                            DateOfVisit = DateTime.Now,
                            TotalAmount = 3200.00m,
                            PaymentStatus = "Unpaid"
                        },
                        new PatientBill
                        {
                            PatientName = "Bronya Rand",
                            PatientEmail = "bronya.rr@mail.com",
                            AssessmentStatus = "For Discharge",
                            DateOfVisit = DateTime.Now.AddDays(-4),
                            TotalAmount = 1750.25m,
                            PaymentStatus = "Paid",
                            PaymentDate = DateTime.Now.AddDays(-3)
                        }
                    };

                    await context.PatientBills.AddRangeAsync(sampleBills);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding sample patient bills: {ex.Message}");
            }
        }
    }
}
