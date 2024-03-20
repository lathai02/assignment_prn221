using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using ScheduleManager.Utils.Enum;

namespace ScheduleManager.Logics.CheckValidRecord
{
    public class CheckValid
    {
        public string[] _CurrentRecord { get; set; }
        public List<RootDataValid> _ListRootData { get; set; }
        public string? Message { get; set; } = null;

        private ScheduleManagerContext _context { get; set; }

        public CheckValid(string[] CurrentRecord, List<RootDataValid> ListRootData, ScheduleManagerContext context)
        {
            _CurrentRecord = CurrentRecord;
            _ListRootData = ListRootData;
            _context = context;
        }

        public bool Condition()
        {
            foreach (var rd in _ListRootData)
            {
                bool result = true;

                // not enough property
                if (_CurrentRecord.Length != 5)
                {
                    Message = "not enough property";
                    result = false;
                }

                // same class, same subject
                if (rd.Class == _CurrentRecord[0] && rd.Subject == _CurrentRecord[1])
                {
                    Message = "same class, same subject";
                    result = false;
                }

                // same room, same time slot
                if (rd.Room == _CurrentRecord[2] && rd.TimeSlot == _CurrentRecord[4])
                {
                    Message = "same room, same time slot";
                    result = false;
                }

                // same class, same timeslot
                if (rd.Class == _CurrentRecord[0] && rd.TimeSlot == _CurrentRecord[4])
                {
                    Message = "same class, same timeslot";
                    result = false;
                }

                // same time slot, same teacher
                if (rd.TimeSlot == _CurrentRecord[4] && rd.Teacher == _CurrentRecord[3])
                {
                    Message = "same time slot, same teacher";
                    result = false;
                }

                // class contain in db
                if (_context.GroupClasses.FirstOrDefault(gl => gl.Name != _CurrentRecord[0]) == null)
                {
                    Message = "class not contain in database";
                    result = false;
                }

                // subject not contain in db
                if (_context.Subjects.FirstOrDefault(s => s.Name != _CurrentRecord[1]) == null)
                {
                    Message = "subject not contain in database";
                    result = false;
                }

                // room not contain in db
                if (_context.Rooms.FirstOrDefault(r => r.Name != _CurrentRecord[2]) == null)
                {
                    Message = "room not contain in database";
                    result = false;
                }

                // teacher not contain in db
                if (_context.Teachers.FirstOrDefault(t => t.Name != _CurrentRecord[3]) == null)
                {
                    Message = "teacher not contain in database";
                    result = false;
                }

                // Time slot is invalid
                if (!Enum.IsDefined(typeof(TimeSlot), _CurrentRecord[4]))
                {
                    Message = "Timeslot is invalid";
                    result = false;
                }

                if (!result)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
