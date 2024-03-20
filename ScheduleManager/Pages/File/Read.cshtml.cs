using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ScheduleManager.Logics.File;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Pages.File
{
    public class ReadModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ReadModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IFormFile? UploadedFile { get; set; }

        List<RootDataValid>? ValidData { get; set; }
        List<RootDataError>? ErrorData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile UploadedFile)
        {
            if (UploadedFile != null && UploadedFile.Length > 0)
            {
                string tempFolderPath = Path.Combine(_environment.WebRootPath, "File");
                Directory.CreateDirectory(tempFolderPath);
                string filePath = Path.Combine(tempFolderPath, UploadedFile.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(fileStream);
                }

                ReadFile rf = new ReadFile();
                rf.Read(filePath);
                ValidData = rf._ListRootData;
                ErrorData = rf._DataError;

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
