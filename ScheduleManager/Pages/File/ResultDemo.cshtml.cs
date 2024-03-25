using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.File
{
    public class ResultDemoModel : PageModel
    {

        private readonly ScheduleManagerContext _context;
        public List<Models.RootDataValid>? validData {  get; set; }
        public List<Models.RootDataError>? dataError {  get; set; }

        public ResultDemoModel(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            validData = _context.RootDataValids.ToList();
            dataError = _context.RootDataErrors.ToList();
        }

        public IActionResult OnPost(string validData)
        {
            return RedirectToPage("/Timetable/Generate");
        }
    }
}
