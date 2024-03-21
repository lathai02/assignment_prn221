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

        public void InsertData(List<RootDataValid> listDataValid)
        {
            foreach (var item in listDataValid)
            {
                char[] characters = item.TimeSlot.ToCharArray();


            }
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
