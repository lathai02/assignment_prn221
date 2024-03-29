﻿using System;
using System.Collections.Generic;

namespace ScheduleManager.Models
{
    public partial class GroupClass
    {
        public GroupClass()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
