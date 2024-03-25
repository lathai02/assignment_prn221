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

        public GenerateModel(InsertToDB insertToDb)
        {
            _insertToDb = insertToDb;
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
            _insertToDb.InsertData(NumberOfWeek, StartDate);

            return RedirectToPage("/Timetable/Schedule");
        }
    }
}
