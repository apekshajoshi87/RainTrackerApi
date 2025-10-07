using RainTrackerApi.Models;

namespace RainTrackerApi.Service
{
    public interface IRainRecordServiceProvider
    {
        /// <summary>
        /// Create Rain Record Service Provider
        /// </summary>
        /// <param name="rainRecord">rainRecord model</param>
        /// <param name="userId">userId from header</param>
        /// <returns>response containing raindata</returns>
        Task<RainRecord?> CreateRainRecordAsync(RainRecord rainRecord, string userId);

        /// <summary>
        /// Fetch Rain Records for a user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Response contains list of rain data for a userId</returns>
        Task<List<RainRecord>?> GetRainRecordAsync(string userId);
    }
}
