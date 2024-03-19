using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.CheckValidRecord
{
    public class CheckValid
    {
        public string[] _CurrentRecord { get; set; }
        public List<RootData> _ListRootData { get; set; }


        public CheckValid(string[] CurrentRecord, List<RootData> ListRootData)
        {
            _CurrentRecord = CurrentRecord;
            _ListRootData = ListRootData;
        }

        public bool Condition()
        {
            foreach (var rd in _ListRootData)
            {
                bool result = true;

                // same class, same subject
                if (rd.Class == _CurrentRecord[0] && rd.Subject == _CurrentRecord[1])
                {
                    result = false;
                }

                // same room, same time slot
                if (rd.Room == _CurrentRecord[2] && rd.TimeSlot == _CurrentRecord[4])
                {
                    result = false;
                }

                // same class, same timeslot
                if (rd.Class == _CurrentRecord[0] && rd.TimeSlot == _CurrentRecord[4])
                {
                    result = false;
                }

                // same time slot, same teacher
                if (rd.TimeSlot == _CurrentRecord[4] && rd.Teacher == _CurrentRecord[3])
                {
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
