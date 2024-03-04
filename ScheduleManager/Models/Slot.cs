using System;
using System.Collections.Generic;

namespace ScheduleManager.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Details { get; set; } = null!;
        public string Slot1 { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
