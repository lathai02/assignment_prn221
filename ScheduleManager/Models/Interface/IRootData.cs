namespace ScheduleManager.Models.Interface
{
    public interface IRootData
    {
        public string? Class { get; set; }
        public string? Subject { get; set; }
        public string? Room { get; set; }
        public string? Teacher { get; set; }
        public string? TimeSlot { get; set; }
    }
}
