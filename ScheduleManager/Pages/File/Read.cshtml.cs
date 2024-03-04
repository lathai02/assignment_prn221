using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleManager.Pages.File
{
    public class ReadModel : PageModel
    {
        [BindProperty]
        public string? filePath { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (filePath != null)
            {

            }
            else
            {
                RedirectToPage();
            }
        }
    }
}
