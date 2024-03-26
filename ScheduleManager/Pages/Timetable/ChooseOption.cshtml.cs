using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleManager.Pages.Timetable
{
    public class ChooseOptionModel : PageModel
    {
        public int slotIdClone { get; set; }
        public void OnGet(int slotId)
        {
            slotIdClone = slotId;
        }
    }
}
