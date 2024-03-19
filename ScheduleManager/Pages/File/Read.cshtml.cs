using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.File
{
    public class ReadModel : PageModel
    {
        [BindProperty]
        public string? FilePath { get; set; }

        List<RootData>? ValidData { get; set; }

        List<RootData>? DataError { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (FilePath != null)
            {
                
            }
            else
            {
                RedirectToPage();
            }
        }




    }
}
