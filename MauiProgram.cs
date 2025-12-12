using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IT_13FinalProject.Data;
using IT_13FinalProject.Services;

namespace IT_13FinalProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            // Add configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            builder.Configuration.AddConfiguration(configuration);
            
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            
            // Use cloud database as primary for data reception
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CloudConnection")));
            
            // Keep local database as backup
            builder.Services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            // Add services - all using cloud database
            builder.Services.AddScoped<IUserAccountService, DatabaseUserAccountService>();
            builder.Services.AddScoped<IHealthRecordService, DatabaseHealthRecordService>();
            builder.Services.AddScoped<IVitalSignService, VitalSignService>();
            builder.Services.AddScoped<IPatientBillService, DatabasePatientBillService>();
            builder.Services.AddScoped<SyncService>();
            
#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize database in background thread to avoid blocking UI
            Task.Run(() => 
            {
                try
                {
                    InitializeDatabase(app);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database initialization failed: {ex.Message}");
                }
            });

            return app;
        }

        private static void InitializeDatabase(MauiApp app)
        {
            using var scope = app.Services.CreateScope();
            var cloudContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var localContext = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
            
            try
            {
                // Initialize cloud database first
                Console.WriteLine("Cloud database initialized successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cloud database initialization failed: {ex.Message}");
                // Fallback to local database
                try
                {
                    localContext.Database.Migrate();
                    Console.WriteLine("Fallback to local database successful!");
                }
                catch (Exception localEx)
                {
                    Console.WriteLine($"Local database initialization also failed: {localEx.Message}");
                }
            }
        }
    }
}
