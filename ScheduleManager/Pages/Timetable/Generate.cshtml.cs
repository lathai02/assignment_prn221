using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScheduleManager.Logics.InsertToDatabase;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.Timetable
{
    public class GenerateModel : PageModel
    {
        private readonly InsertToDB _insertToDb;
        private readonly ScheduleManagerContext _context;

        public GenerateModel(InsertToDB insertToDb, ScheduleManagerContext context)
        {
            _insertToDb = insertToDb;
            _context = context;
        }

        [BindProperty]
        public int NumberOfWeek { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            AutoGenScheduleDatum autoGenScheduleDatum = new AutoGenScheduleDatum
            {
                NumberOfWeek = NumberOfWeek,
                StartDate = StartDate,
            };

            _context.AutoGenScheduleData.Add(autoGenScheduleDatum);
            _context.SaveChanges();
            _insertToDb.InsertData(NumberOfWeek, StartDate);

            return RedirectToPage("/Timetable/Schedule");
        }
    }
}
