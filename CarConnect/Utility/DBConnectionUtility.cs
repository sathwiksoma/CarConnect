using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Utility
{
    internal class DBConnectionUtility
    {
        private static IConfiguration _iConfiguration;

        static DBConnectionUtility()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
            _iConfiguration = builder.Build();
        }

        public static string GetConnectedString()
        {
            return _iConfiguration.GetConnectionString("LocalConnectionString");
        }
    }
}
