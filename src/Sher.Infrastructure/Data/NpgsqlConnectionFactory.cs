using System;
using System.Data;
using Npgsql;
using Sher.Application.Configuration;

namespace Sher.Infrastructure.Data
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _dbConnection;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_dbConnection is not null) return _dbConnection;

            _dbConnection = new NpgsqlConnection(_connectionString);
            _dbConnection.Open();

            return _dbConnection;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}