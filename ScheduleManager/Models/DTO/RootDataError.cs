﻿using ScheduleManager.Models.Interface;

namespace ScheduleManager.Models.DTO
{
    public class RootDataError : IRootData
    {
        public string Message { get; set; } = "";
        public string? Class { get; set; }
        public string? Subject { get; set; }
        public string? Room { get; set; }
        public string? Teacher { get; set; }
        public string? TimeSlot { get; set; }
        public string? MissData { get; set; } = null;
    }
}
