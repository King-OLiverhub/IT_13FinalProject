using Microsoft.Extensions.Configuration;
using System;

namespace IT_13FinalProject.Services
{
    public interface IConfigurationService
    {
        string GetConnectionString();
    }

    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetConnectionString()
        {
            // Ensure we never return null — throw a clear exception if missing
            return _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing connection string 'DefaultConnection' in configuration.");
        }
    }
}