using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Sher.Api;
using Sher.IntegrationTests.Utils;

namespace Sher.IntegrationTests.Fixtures
{
    public class TestBaseFixture
    {
        public TestServerFactory<Startup> ServerFactory { get; }
        public RegisteredUserFixture RegisteredUserFixture { get; }

        private readonly string _connectionString;

        public TestBaseFixture()
        {
            ServerFactory = new TestServerFactory<Startup>();
            RegisteredUserFixture = new RegisteredUserFixture(ServerFactory);

            var cfg = new ConfigurationBuilder()
                .AddTestSources()
                .Build();

            _connectionString = cfg.GetConnectionString("Default");
            
            ResetDb();
        }

        private void ResetDb()
        {
            try
            {
                using var dbConnection = new NpgsqlConnection(_connectionString);

                dbConnection.Open();
                var checkpoint = new Checkpoint
                {
                    SchemasToInclude = new[]
                    {
                        "public"
                    },
                    DbAdapter = DbAdapter.Postgres
                };
                checkpoint.Reset(dbConnection).Wait();
            }
            catch (Exception e)
            {
                switch (e)
                {
                    // this is expected to throw when the db hasn't been created yet
                    case PostgresException pgEx when pgEx.ToString() == PostgresErrorCodes.InvalidCatalogName:
                    case AggregateException agEx when agEx.InnerExceptions.Any(ex => ex is InvalidOperationException):
                        break;
                    default:
                        throw;
                }
            }
        }
    }
}