using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using System.Linq.Expressions;

namespace ScheduleManager.Logics.InsertToDatabase
{
    public class InsertToDB
    {
        private readonly ScheduleManagerContext _context;

        public InsertToDB(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void InsertData(List<RootDataValid> listDataValid, int numberOfWeek, DateTime startDate)
        {
            List<DataOneWeek> dataOneWeek = new List<DataOneWeek>();

            foreach (var item in listDataValid)
            {
                char[] characters = item.TimeSlot.ToCharArray();

                var timeSlotDetail = GetTimeSlotDetail(characters);
                DataOneWeek dow1 = new DataOneWeek
                {
                    Class = item.Class,
                    Subject = item.Subject,
                    Teacher = item.Teacher,
                    Room = item.Room,
                    DayOfWeek = timeSlotDetail.Item2,
                    Slot = timeSlotDetail.Item1
                };

                DataOneWeek dow2 = new DataOneWeek
                {
                    Class = item.Class,
                    Subject = item.Subject,
                    Teacher = item.Teacher,
                    Room = item.Room,
                    DayOfWeek = timeSlotDetail.Item4,
                    Slot = timeSlotDetail.Item3
                };

                dataOneWeek.Add(dow1);
                dataOneWeek.Add(dow2);
            }

            var timeTable = AutoGenerateTimeTable(numberOfWeek, startDate, dataOneWeek);

            _context.Schedules.AddRange(timeTable);
            _context.SaveChanges();
        }

        private List<Schedule> AutoGenerateTimeTable(int numberOfWeek, DateTime startDate, List<DataOneWeek> dataOneWeek)
        {
            List<Schedule> timeTable = new List<Schedule>();

            int weekExtraSlot = 0;
            for (int i = 0; i < numberOfWeek; i++)
            {
                for (int j = 0; j < dataOneWeek.Count(); j++)
                {
                    int dayToAdd = (int)dataOneWeek[j].DayOfWeek - (int)startDate.DayOfWeek + (i * 7);

                    DateTime specificDate = default;
                    if (dayToAdd >= 0)
                    {
                        specificDate = startDate.AddDays(dayToAdd);
                    }
                    else
                    {
                        specificDate = startDate.AddDays(dayToAdd + (numberOfWeek * 7));
                    }

                    var idTuple = GetId(dataOneWeek[j].Class, dataOneWeek[j].Room, dataOneWeek[j].Slot, dataOneWeek[j].Subject, dataOneWeek[j].Teacher);

                    Schedule schedule = new Schedule
                    {
                        ClassId = idTuple.Item1,
                        RoomId = idTuple.Item2,
                        TimeSlotId = idTuple.Item3,
                        SubjectId = idTuple.Item4,
                        TeacherId = idTuple.Item5,
                        Date = specificDate
                    };

                    timeTable.Add(schedule);
                }
            }

            return timeTable;
        }

        private (int, int, int, int, int) GetId(string className, string roomName, int? slotName, string subjectName, string teacherName)
        {
            int classId = _context.GroupClasses.FirstOrDefault(c => c.Name == className).Id;
            int roomId = _context.Rooms.FirstOrDefault(r => r.Name == roomName).Id;
            int slotId = _context.Slots.FirstOrDefault(s => s.Slot1 == slotName.ToString()).Id;
            int subjectId = _context.Subjects.FirstOrDefault(sj => sj.Name == subjectName).Id;
            int teacherId = _context.Teachers.FirstOrDefault(t => t.Name == teacherName).Id;

            return (classId, roomId, slotId, subjectId, teacherId);
        }

        private (int?, DayOfWeek?, int?, DayOfWeek?) GetTimeSlotDetail(char[] characters)
        {
            if (characters != null && characters.Length > 0)
            {
                if (characters[0] == 'A')
                {
                    DayOfWeek? dayOfWeekFirst = GetDayOfWeek(characters[1]);
                    DayOfWeek? dayOfWeekSecond = GetDayOfWeek(characters[2]);

                    return (1, dayOfWeekFirst, 2, dayOfWeekSecond);
                }
                else if (characters[0] == 'P')
                {
                    DayOfWeek? dayOfWeekFirst = GetDayOfWeek(characters[1]);
                    DayOfWeek? dayOfWeekSecond = GetDayOfWeek(characters[2]);

                    return (3, dayOfWeekFirst, 4, dayOfWeekSecond);
                }
                else
                {
                    return (null, null, null, null);
                }
            }
            else
            {
                return (null, null, null, null);
            }
        }

        private DayOfWeek? GetDayOfWeek(char character)
        {
            string dayOfWeekString = character.ToString();
            int dayOfWeekNumber = int.Parse(dayOfWeekString);

            DayOfWeek? dayOfWeek = null;
            switch (dayOfWeekNumber)
            {
                case 1:
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                case 2:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case 3:
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case 4:
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case 5:
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case 6:
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case 7:
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
            };

            return dayOfWeek;
        }
    }
}
