using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using System.Security.Claims;
using System.Text.Json;

namespace ScheduleManager.Logics.File
{
    public class FileHandle
    {
        private readonly CheckValid _checkValid;
        private readonly IWebHostEnvironment _environment;
        private readonly ScheduleManagerContext _context;


        public FileHandle(CheckValid checkValid, IWebHostEnvironment environment, ScheduleManagerContext context)
        {
            _checkValid = checkValid;
            _environment = environment;
            _context = context;
        }

        public void Read(string filePath)
        {
            List<Models.RootDataValid>? listRootData = new List<Models.RootDataValid>();
            List<Models.RootDataError>? listDataError = new List<Models.RootDataError>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    var result = _checkValid.Condition(values, listRootData);

                    if (result.Item1)
                    {
                        Models.RootDataValid rd = new Models.RootDataValid
                        {
                            Class = values[0],
                            Subject = values[1],
                            Room = values[2],
                            Teacher = values[3],
                            TimeSlot = values[4],
                        };
                        listRootData.Add(rd);
                    }
                    else
                    {
                        Models.RootDataError? rd = null;
                        if (result.Item3)
                        {
                            rd = new Models.RootDataError
                            {
                                MissData = string.Join(",", values),
                                Message = result.Item2 ?? "loi roi"
                            };
                        }
                        else
                        {
                            rd = new Models.RootDataError
                            {
                                Class = values[0] ?? "",
                                Subject = values[1] ?? "",
                                Room = values[2] ?? "",
                                Teacher = values[3] ?? "",
                                TimeSlot = values[4] ?? "",
                                Message = result.Item2 ?? "loi roi"
                            };
                        }
                        listDataError.Add(rd);
                    }
                }

                _context.RootDataValids.AddRange(listRootData);
                _context.RootDataErrors.AddRange(listDataError);
                _context.SaveChanges();
            }
        }

        public async Task<string> Import(IFormFile UploadedFile)
        {
            string tempFolderPath = Path.Combine(_environment.WebRootPath, "File");
            Directory.CreateDirectory(tempFolderPath);
            string filePath = Path.Combine(tempFolderPath, UploadedFile.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
