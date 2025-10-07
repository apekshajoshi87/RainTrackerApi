using System.Data;

namespace RainTrackerApi.Data.DataProviders
{
    public interface IBaseDataProvider
    {
        /// <summary>
        /// Opens and returns an active db connection.
        /// </summary>
        Task<IDbConnection> OpenConnectionAsync();

        /// <summary>
        /// Executes a query and returns the result.
        /// </summary>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
    }
}
