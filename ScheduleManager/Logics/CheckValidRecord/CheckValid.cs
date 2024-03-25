using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using ScheduleManager.Utils.Enum;

namespace ScheduleManager.Logics.CheckValidRecord
{
    public class CheckValid
    {
        private readonly ScheduleManagerContext _context;

        public CheckValid(ScheduleManagerContext context)
        {
            _context = context;
        }

        public (bool, string?, bool) Condition(string[] _CurrentRecord, List<RootDataValid> _ListRootData)
        {
            string? message = "";
            bool isValid = true;
            bool flag = false;

            // not enough property
            if (_CurrentRecord.Length != 5)
            {
                flag = true;
                message = "not enough property";
                return (false, message, flag);
            }

            // class contain in db
            if (_context.GroupClasses.FirstOrDefault(gl => gl.Name == _CurrentRecord[0]) == null)
            {
                message = "class not contain in database";
                isValid = false;
            }

            // subject not contain in db
            if (_context.Subjects.FirstOrDefault(s => s.Name == _CurrentRecord[1]) == null)
            {
                message = "subject not contain in database";
                isValid = false;
            }

            // room not contain in db
            if (_context.Rooms.FirstOrDefault(r => r.Name == _CurrentRecord[2]) == null)
            {
                message = "room not contain in database";
                isValid = false;
            }

            // teacher not contain in db
            if (_context.Teachers.FirstOrDefault(t => t.Name == _CurrentRecord[3]) == null)
            {
                message = "teacher not contain in database";
                isValid = false;
            }

            // Time slot is invalid
            if (!Enum.IsDefined(typeof(TimeSlot), _CurrentRecord[4]))
            {
                message = "Timeslot is invalid";
                isValid = false;
            }

            if (!isValid)
            {
                return (false, message, flag);
            }

            foreach (var rd in _ListRootData)
            {
                // same class, same subject
                if (rd.Class == _CurrentRecord[0] && rd.Subject == _CurrentRecord[1])
                {
                    message = "same class, same subject";
                    isValid = false;
                }

                // same room, same time slot
                if (rd.Room == _CurrentRecord[2] && rd.TimeSlot == _CurrentRecord[4])
                {
                    message = "same room, same time slot";
                    isValid = false;
                }

                // same class, same timeslot
                if (rd.Class == _CurrentRecord[0] && rd.TimeSlot == _CurrentRecord[4])
                {
                    message = "same class, same timeslot";
                    isValid = false;
                }

                // same time slot, same teacher
                if (rd.TimeSlot == _CurrentRecord[4] && rd.Teacher == _CurrentRecord[3])
                {
                    message = "same time slot, same teacher";
                    isValid = false;
                }
            }

            return (true, message, flag);
        }
    }
}
