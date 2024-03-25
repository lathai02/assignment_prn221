using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Logics.InsertToDatabase;
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
        public List<RootDataValid> ValidData { get; set; }

        [BindProperty]
        public string ValidDataClone { get; set; }

        [BindProperty]
        public int NumberOfWeek { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }


        public void OnGet()
        {
            string validDataJson = TempData["ValidData"] as string;
            ValidData = JsonConvert.DeserializeObject<List<RootDataValid>>(validDataJson);
        }

        public IActionResult OnPost()
        {
            _insertToDb.InsertData(JsonConvert.DeserializeObject<List<RootDataValid>>(ValidDataClone), NumberOfWeek, StartDate);

            return RedirectToPage("/Timetable/Schedule");
        }
    }
}
