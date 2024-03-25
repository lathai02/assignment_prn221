using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using static System.Reflection.Metadata.BlobBuilder;

namespace ScheduleManager.Pages.Timetable
{
    public class ScheduleModel : PageModel
    {
        private readonly ScheduleManagerContext _context;
        public List<Schedule> Schedules { get; set; }
        public List<Models.Slot> Slots { get; set; }
        public SelectList listDateRange { get; set; }
        public List<string> listDateRangeOneWeek { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateRange dateRange { get; set; }

        public ScheduleModel(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var startDate = _context.Schedules.Min(s => s.Date);
            var endDate = _context.Schedules.Max(s => s.Date);
            var weeks = GetDateRange(startDate, endDate);
            listDateRange = new SelectList(weeks, nameof(DateRange.StartDate), nameof(DateRange.Range));

            DateTime startDateReal = findMonday(startDate);
            listDateRangeOneWeek = GetDateRangeOneWeek(startDateReal);
            Slots = _context.Slots.ToList();
            Schedules = _context.Schedules.Include(sche => sche.Teacher).Include(sche => sche.Subject).Include(sche => sche.Room).Include(sche => sche.Class).Include(sche => sche.TimeSlot).Where(s => s.Date >= startDateReal && s.Date <= startDateReal.AddDays(6)).ToList();
        }

        private List<string> GetDateRangeOneWeek(DateTime startDate)
        {
            List<string> dateRangeOneWeek = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                dateRangeOneWeek.Add(startDate.ToString("dd-MM-yyyy"));
                startDate = startDate.AddDays(1);
            }
            return dateRangeOneWeek;
        }

        private List<DateRange> GetDateRange(DateTime startDate, DateTime endDate)
        {

            List<DateRange> weeks = new List<DateRange>();
            DateTime startDateReal = findMonday(startDate);

            TimeSpan span = findSunday(endDate) - startDateReal;
            int totalWeeks = span.Days / 7;

            string currentWeek = "";

            DateTime currentWeekStart = startDateReal;
            DateTime currentWeekEnd = startDateReal.AddDays(6);
            currentWeek = currentWeekStart.ToString("dd/MM/yyyy") + " - " + currentWeekEnd.ToString("dd/MM/yyyy");
            DateRange dr = new DateRange
            {
                Range = currentWeek,
                StartDate = currentWeekStart,
                EndDate = currentWeekEnd,
            };
            weeks.Add(dr);

            for (int i = 0; i < totalWeeks; i++)
            {
                currentWeekStart = currentWeekStart.AddDays(7);
                currentWeekEnd = currentWeekStart.AddDays(6);
                currentWeek = currentWeekStart.ToString("dd/MM/yyyy") + " - " + currentWeekEnd.ToString("dd/MM/yyyy");

                DateRange drFor = new DateRange
                {
                    Range = currentWeek,
                    StartDate = currentWeekStart,
                    EndDate = currentWeekEnd,
                };
                weeks.Add(drFor);
            }

            return weeks;
        }

        private DateTime findMonday(DateTime startDate)
        {
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            return startDate;
        }

        private DateTime findSunday(DateTime endDate)
        {
            while (endDate.DayOfWeek != DayOfWeek.Sunday)
            {
                endDate = endDate.AddDays(1);
            }

            return endDate;
        }
    }
}
