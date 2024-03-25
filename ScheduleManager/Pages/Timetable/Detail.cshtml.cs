using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Models;

namespace ScheduleManager.Pages.Timetable
{
    public class DetailModel : PageModel
    {
        private readonly ScheduleManagerContext _context;

        public Schedule slot {  get; set; }

        public DetailModel(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void OnGet(int slotId)
        {
           slot = _context.Schedules.FirstOrDefault(s => s.Id == slotId);
        }
    }
}
