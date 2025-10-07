using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RainTrackerApi.Models;
using RainTrackerApi.Service;

namespace RainTrackerApi.Controllers
{
    [ApiController]
    [Route("/api/data")]
    public class RainRecordController : ControllerBase
    {
        private readonly ILogger<RainRecordController> _logger;
        private IRainRecordServiceProvider _rainServiceProvider;

        public RainRecordController(ILogger<RainRecordController> logger, IRainRecordServiceProvider rainServiceProvider)
        {
            _logger = logger;
            _rainServiceProvider = rainServiceProvider;
        }

        /// <summary>
        /// Post endpoint to create rain data.
        /// </summary>
        /// <param name="rainRecord">rain record model</param>
        /// <param name="userId">userid from header</param>
        /// <returns>returns response with appropriate data</returns>
        [HttpPost(Name = "CreateRainData")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRainDataAsync([FromBody] RainRecord rainRecord, [FromHeader(Name = "x-userId"), BindRequired] string userId)
        {
            try
            {
                if (rainRecord == null)
                {
                    return BadRequest("Invalid payload.");
                }

                var result = await _rainServiceProvider.CreateRainRecordAsync(rainRecord, userId);
                var locationUrl = $"/api/data/{userId}";
                return Created(locationUrl, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating rain data for user {UserId}.", userId);
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get endpoint to fetch rain data as per userId
        /// </summary>
        /// <param name="userId">userid from header</param>
        /// <returns>response contains rain data</returns>
        [HttpGet(Name = "GetRainData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRainDataAsync([FromHeader(Name = "x-userId"), BindRequired] string userId)
        {
            try
            {
                var rainRecord = await _rainServiceProvider.GetRainRecordAsync(userId!);
                return Ok(rainRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching rain data for user {UserId}.", userId);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
