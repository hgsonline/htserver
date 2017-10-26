using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace HTServer.Models
{
    public class AppDb : IDisposable
    {
        public static IConfigurationRoot Configuration { get; set; }
        public string AppConfig()
        {

            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("myConnectionString");
        }

        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection(AppConfig());
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}