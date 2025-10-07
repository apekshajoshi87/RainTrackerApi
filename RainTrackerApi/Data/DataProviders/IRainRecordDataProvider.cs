using RainTrackerApi.Models;

namespace RainTrackerApi.Data.DataProviders
{
    public interface IRainRecordDataProvider
    {
        /// <summary>
        /// Creates rain data for a payload
        /// </summary>
        /// <param name="rainRecord">rainrecord model</param>
        /// <param name="userId">userId from header</param>
        /// <returns>response containing rain data</returns>
        Task<RainRecord?> CreateRainRecordAsync(RainRecord rainRecord, string userId);

        /// <summary>
        /// Fetches all rain data for a specific user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of rain information for a specific user</returns>
        Task<List<RainRecord>?> GetRainRecordAsync(string userId);
    }
}
