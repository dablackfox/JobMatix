using System;
using System.Data;
using Npgsql;

namespace JMxPOS_Avalonia
{
    public static class DatabaseHelper
    {
        private static string? _connectionString;

        public static void Initialize()
        {
            // Load from .env file if available
            var envPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            if (System.IO.File.Exists(envPath))
            {
                foreach (var line in System.IO.File.ReadAllLines(envPath))
                {
                    if (line.StartsWith("DB_CONNECTION_STRING_POSTGRES="))
                    {
                        _connectionString = line.Substring("DB_CONNECTION_STRING_POSTGRES=".Length).Trim();
                        break;
                    }
                }
            }

            // Default connection string
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = "Host=localhost;Port=5432;Database=jobmatix_pos;Username=jobmatix_user;Password=JobMatix2026!Dev";
            }
        }

        public static IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
                Initialize();

            return new NpgsqlConnection(_connectionString);
        }

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                Initialize();

            return _connectionString!;
        }
    }
}
