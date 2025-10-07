using Npgsql;
using System.Data;
using Dapper;

namespace RainTrackerApi.Data.DataProviders
{
    public class BaseDataProvider : IBaseDataProvider
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public BaseDataProvider(IConfiguration config) 
        {
            _config = config;
            _connectionString = _config.GetConnectionString("database") ?? throw new InvalidOperationException("Connection string not set.");
        }

        /// <summary>
        /// Opens and returns an active db connection.
        /// </summary>
        public async Task<IDbConnection> OpenConnectionAsync()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Executes query and returns the result.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            using var connection = await OpenConnectionAsync();
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }
}
