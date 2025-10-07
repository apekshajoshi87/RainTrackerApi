namespace RainTrackerApi.Models
{
    public class RainRecord
    {
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime DateTimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Did it rain or not
        /// </summary>
        public required bool ItRained { get; set; }
    }
}
