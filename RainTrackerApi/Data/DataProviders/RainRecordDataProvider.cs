using RainTrackerApi.Models;
using Dapper;

namespace RainTrackerApi.Data.DataProviders
{
    public class RainRecordDataProvider : BaseDataProvider, IRainRecordDataProvider
    {
        private readonly ILogger<RainRecordDataProvider> _logger;
        public RainRecordDataProvider(IConfiguration configuration, ILogger<RainRecordDataProvider> logger) : base(configuration)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates rain data for a payload
        /// </summary>
        /// <param name="rainRecord">rainRecord Model</param>
        /// <returns>response containing rain data</returns>
        /// <exception cref="ArgumentNullException">rain payload cannot be null</exception>
        /// <exception cref="InvalidOperationException">Invalid operation while inserting data into postgres</exception>
        public async Task<RainRecord?> CreateRainRecordAsync(RainRecord rainRecord, string userId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(rainRecord);

                var param = new
                {
                    datetimestamp = rainRecord.DateTimeStamp,
                    itrained = rainRecord.ItRained,
                    userid = userId
                };

                var sql = @"
                    INSERT INTO rain (datetimestamp, itrained, userid)
                    VALUES (@datetimestamp, @itrained, @userid)
                    RETURNING rainid;";

                var result = await QueryAsync<int>(sql, param);
                
                _logger.LogInformation("Rain record created successfully for user {UserId}", userId);
                return rainRecord;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create record.");
                throw new InvalidOperationException($"Failed to create record: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Fetches all rain data for a specific user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>A list of rain information for a specific user</returns>
        public async Task<List<RainRecord>?> GetRainRecordAsync(string userId)
        {
            try
            {
                var sql = "SELECT * FROM rain WHERE userid = @userid";
                var result = await QueryAsync<RainRecord>(sql, new { userId });
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching rain data for {UserId}.", userId);
                throw;
            }
        }
    }
}
