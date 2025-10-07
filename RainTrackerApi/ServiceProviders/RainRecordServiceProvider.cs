using RainTrackerApi.Data.DataProviders;
using RainTrackerApi.Models;
using RainTrackerApi.Service;

namespace RainTrackerApi.ServiceProviders
{
    public class RainRecordServiceProvider : IRainRecordServiceProvider
    {
        private IRainRecordDataProvider _rainRecordDataProvider;
        private readonly IConfiguration _configuration;
        public RainRecordServiceProvider(IRainRecordDataProvider rainRecordDataProvider, IConfiguration configuration)
        {
            _rainRecordDataProvider = rainRecordDataProvider;
            _configuration = configuration;
        }

        /// <summary>
        /// Create Rain Record Service Provider
        /// </summary>
        /// <param name="rainRecord">rainRecord model</param>
        /// <param name="userId">userId from header</param>
        /// <returns>response containing raindata</returns>
        public async Task<RainRecord?> CreateRainRecordAsync(RainRecord rainRecord, string userId)
        { 
            return await _rainRecordDataProvider.CreateRainRecordAsync(rainRecord, userId);
        }

        /// <summary>
        /// Fetch Rain Records for a user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Response contains list of rain data for a userId</returns>
        public async Task<List<RainRecord>?> GetRainRecordAsync(string userId)
        { 
            return await _rainRecordDataProvider.GetRainRecordAsync(userId);
        }
    }
}
