using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using IT_13FinalProject.Data;

namespace IT_13FinalProject.Services
{
    public class DatabaseUpdateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DatabaseUpdateService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task EnsureInventoryColumnsAsync()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine($"Connecting to database with connection string: {connectionString}");
                
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                Console.WriteLine("Database connection opened successfully.");

                // Update inventory table
                await UpdateInventoryTableAsync(connection);

                // Update patients table
                await UpdatePatientsTableAsync(connection);

                // Update consultations table
                await UpdateConsultationsTableAsync(connection);

                Console.WriteLine("Database schema updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating database schema: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private async Task UpdateInventoryTableAsync(SqlConnection connection)
        {
            // List of columns to add with their SQL definitions
            var columnsToAdd = new Dictionary<string, string>
            {
                { "generic_name", "NVARCHAR(200) NULL" },
                { "brand_name", "NVARCHAR(200) NULL" },
                { "category", "NVARCHAR(100) NULL" },
                { "unit", "NVARCHAR(50) NOT NULL DEFAULT 'Piece(s)'" },
                { "batch_number", "NVARCHAR(100) NULL" },
                { "storage_requirements", "NVARCHAR(200) NOT NULL DEFAULT 'Room temperature'" },
                { "prescription_required", "BIT NOT NULL DEFAULT 0" },
                { "barcode", "NVARCHAR(100) NULL" },
                { "is_archived", "BIT NOT NULL DEFAULT 0" },
                { "archived_at", "DATETIME NULL" }
            };

            foreach (var column in columnsToAdd)
            {
                // Check if column exists
                var checkColumnSql = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'inventory' AND COLUMN_NAME = @ColumnName";

                using var checkCommand = new SqlCommand(checkColumnSql, connection);
                checkCommand.Parameters.AddWithValue("@ColumnName", column.Key);
                var columnExists = (int)await checkCommand.ExecuteScalarAsync() > 0;

                if (!columnExists)
                {
                    // Add the column
                    var addColumnSql = $"ALTER TABLE inventory ADD {column.Key} {column.Value}";
                    using var addCommand = new SqlCommand(addColumnSql, connection);
                    await addCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"Added inventory column: {column.Key}");
                }
            }

            // Update existing records with default values for NOT NULL columns
            var updateSql = @"
                UPDATE inventory SET unit = 'Piece(s)' WHERE unit IS NULL;
                UPDATE inventory SET storage_requirements = 'Room temperature' WHERE storage_requirements IS NULL;
                UPDATE inventory SET prescription_required = 0 WHERE prescription_required IS NULL;
                UPDATE inventory SET is_archived = 0 WHERE is_archived IS NULL;";

            using var updateCommand = new SqlCommand(updateSql, connection);
            await updateCommand.ExecuteNonQueryAsync();
        }

        private async Task UpdatePatientsTableAsync(SqlConnection connection)
        {
            // List of columns to add to patients table
            var columnsToAdd = new Dictionary<string, string>
            {
                { "is_archived", "BIT NOT NULL DEFAULT 0" },
                { "archived_at", "DATETIME NULL" }
            };

            foreach (var column in columnsToAdd)
            {
                // Check if column exists
                var checkColumnSql = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'patients' AND COLUMN_NAME = @ColumnName";

                using var checkCommand = new SqlCommand(checkColumnSql, connection);
                checkCommand.Parameters.AddWithValue("@ColumnName", column.Key);
                var columnExists = (int)await checkCommand.ExecuteScalarAsync() > 0;

                if (!columnExists)
                {
                    // Add the column
                    var addColumnSql = $"ALTER TABLE patients ADD {column.Key} {column.Value}";
                    using var addCommand = new SqlCommand(addColumnSql, connection);
                    await addCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"Added patients column: {column.Key}");
                }
            }

            // Update existing records with default values
            var updateSql = @"
                UPDATE patients SET is_archived = 0 WHERE is_archived IS NULL;";

            using var updateCommand = new SqlCommand(updateSql, connection);
            await updateCommand.ExecuteNonQueryAsync();
        }

        private async Task UpdateConsultationsTableAsync(SqlConnection connection)
        {
            Console.WriteLine("Starting consultations table update...");
            
            // List of columns to add to consultations table
            var columnsToAdd = new Dictionary<string, string>
            {
                { "is_archived", "BIT NOT NULL DEFAULT 0" },
                { "archived_at", "DATETIME NULL" },
                { "doctor_initial_remarks", "NVARCHAR(MAX) NULL" },
                { "special_instructions", "NVARCHAR(MAX) NULL" }
            };

            foreach (var column in columnsToAdd)
            {
                // Check if column exists
                var checkColumnSql = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'consultations' AND COLUMN_NAME = @ColumnName";

                using var checkCommand = new SqlCommand(checkColumnSql, connection);
                checkCommand.Parameters.AddWithValue("@ColumnName", column.Key);
                var columnExists = (int)await checkCommand.ExecuteScalarAsync() > 0;

                Console.WriteLine($"Column {column.Key} exists: {columnExists}");

                if (!columnExists)
                {
                    // Add the column
                    var addColumnSql = $"ALTER TABLE consultations ADD {column.Key} {column.Value}";
                    Console.WriteLine($"Executing: {addColumnSql}");
                    
                    using var addCommand = new SqlCommand(addColumnSql, connection);
                    await addCommand.ExecuteNonQueryAsync();
                    Console.WriteLine($"Added consultations column: {column.Key}");
                }
            }

            // Update existing records with default values
            var updateSql = @"
                UPDATE consultations SET is_archived = 0 WHERE is_archived IS NULL;";

            Console.WriteLine("Updating existing consultations records...");
            using var updateCommand = new SqlCommand(updateSql, connection);
            var rowsAffected = await updateCommand.ExecuteNonQueryAsync();
            Console.WriteLine($"Updated {rowsAffected} consultation records.");
        }
    }
}
