﻿using System;
using System.Collections.Generic;

namespace ScheduleManager.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int RoomId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime Date { get; set; }

        public virtual GroupClass Class { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual Slot TimeSlot { get; set; } = null!;
    }
}
