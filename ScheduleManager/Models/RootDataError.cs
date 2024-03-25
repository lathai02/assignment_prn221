using System;
using System.Collections.Generic;

namespace ScheduleManager.Models
{
    public partial class RootDataError
    {
        public string Message { get; set; } = null!;
        public string? Class { get; set; }
        public string? Subject { get; set; }
        public string? Room { get; set; }
        public string? Teacher { get; set; }
        public string? TimeSlot { get; set; }
        public string? MissData { get; set; }
    }
}
