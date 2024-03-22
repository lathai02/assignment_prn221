namespace ScheduleManager.Models.DTO
{
    public class DataOneWeek
    {
        public string? Class { get; set; } 
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
        public string? Room { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public int? Slot { get; set; }
    }
}
