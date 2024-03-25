using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Logics.File;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.File
{
    public class ReadModel : PageModel
    {
        private readonly FileHandle _readFile;

        public ReadModel(FileHandle readFile)
        {
            _readFile = readFile;
        }

        public IFormFile? UploadedFile { get; set; }

        List<Models.RootDataValid>? ValidData { get; set; }
        List<Models.RootDataError>? ErrorData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile UploadedFile)
        {
            if (UploadedFile != null && UploadedFile.Length > 0)
            {
                var filePath = await _readFile.Import(UploadedFile);
                _readFile.Read(filePath);

                return RedirectToPage("/File/ResultDemo");
            }
            else
            {
                TempData["ErrorMessage"] = "File upload failed.";
                return RedirectToPage("/File/Read");
            }
        }
    }
}
