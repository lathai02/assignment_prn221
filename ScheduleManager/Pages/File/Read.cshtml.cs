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
        List<RootDataError>? ErrorData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile UploadedFile)
        {
            if (UploadedFile != null && UploadedFile.Length > 0)
            {
                var filePath = await _readFile.Import(UploadedFile);
                var result = _readFile.Read(filePath);
                ValidData = result.Item1;
                ErrorData = result.Item2;

                TempData["Message"] = "File uploaded successfully.";
                TempData["ValidData"] = JsonConvert.SerializeObject(ValidData);
                TempData["DataError"] = JsonConvert.SerializeObject(ErrorData);

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
