using System;
using System.Collections.Generic;

namespace ScheduleManager.Models
{
    public partial class AutoGenScheduleDatum
    {
        public int Id { get; set; }
        public int? NumberOfWeek { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
