using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.CheckValidRecord
{
    public class CheckValid
    {
        public string[] _CurrentRecord { get; set; }
        public List<RootDataValid> _ListRootData { get; set; }
        public string? Message { get; set; } = null;

        public CheckValid(string[] CurrentRecord, List<RootDataValid> ListRootData)
        {
            _CurrentRecord = CurrentRecord;
            _ListRootData = ListRootData;
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

                if (!result)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
