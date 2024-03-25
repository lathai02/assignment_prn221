using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.File
{
    public class ResultDemoModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string validData)
        {
            TempData["ValidData"] = validData;

            return RedirectToPage("/Timetable/Generate");
        }
    }
}
