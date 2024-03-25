using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.Timetable
{
    public class GenerateModel : PageModel
    {
        public List<RootDataValid>? ValidData { get; set; }

        [BindProperty]
        public string? ValidDataClone { get; set; }


        public void OnGet()
        {
            string validDataJson = TempData["ValidData"] as string;
            ValidData = JsonConvert.DeserializeObject<List<RootDataValid>>(validDataJson);
        }

        public void OnPost()
        {
            var a = ValidDataClone;
        }
    }
}
