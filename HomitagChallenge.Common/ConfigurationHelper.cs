using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HomitagChallenge.Common
{
    public static class ConfigurationHelper
    {
        static IConfigurationRoot configuration;
        static ConfigurationHelper()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }
        
        public static string GetConnectionString(string key)
        {
            var value = configuration.GetSection("connectionStrings")[key];
            return value;
        }
    }
}
