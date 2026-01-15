using System;
using System.Data;
using Npgsql;

namespace JMxPOS8.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            // Read connection parameters from environment variables (loaded from .env)
            var host = Environment.GetEnvironmentVariable("JOBMATIX_PG_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("JOBMATIX_PG_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("JOBMATIX_PG_DB_POS") ?? "jobmatix_pos";
            var username = Environment.GetEnvironmentVariable("JOBMATIX_PG_USER") ?? "jobmatix_user";
            var password = Environment.GetEnvironmentVariable("JOBMATIX_PG_PASSWORD") ?? "JobMatix2026!Dev";
            
            _connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};Include Error Detail=true";
        }

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public static void LoadEnvironment()
        {
            try
            {
                var envPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    ".env"
                );

                if (System.IO.File.Exists(envPath))
                {
                    foreach (var line in System.IO.File.ReadAllLines(envPath))
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;

                        var parts = line.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading .env file: {ex.Message}");
            }
        }
    }
}
