using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using System.Security.Claims;

namespace ScheduleManager.Logics.File
{
    public class ReadFile
    {
        public List<RootDataValid>? _ListRootData { get; set; }
        public List<RootDataError>? _DataError { get; set; }

        private ScheduleManagerContext _context { get; set; }

        public ReadFile(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void Read(string filePath)
        {
            _ListRootData = new List<RootDataValid>();
            _DataError = new List<RootDataError>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    CheckValid cv = new CheckValid(values, _ListRootData, _context);
                    var isValidRecord = cv.Condition();

                    if (isValidRecord)
                    {
                        RootDataValid rd = new RootDataValid
                        {
                            Class = values[0],
                            Subject = values[1],
                            Room = values[2],
                            Teacher = values[3],
                            TimeSlot = values[4],
                        };
                        _ListRootData.Add(rd);
                    }
                    else
                    {
                        RootDataError? rd = null;
                        if (cv.Flag)
                        {
                            rd = new RootDataError
                            {
                                MissData = string.Join(",", values),
                                Message = cv.Message ?? "loi roi"
                            };
                        }
                        else
                        {
                            rd = new RootDataError
                            {
                                Class = values[0] ?? "",
                                Subject = values[1] ?? "",
                                Room = values[2] ?? "",
                                Teacher = values[3] ?? "",
                                TimeSlot = values[4] ?? "",
                                Message = cv.Message ?? "loi roi"
                            };
                        }
                        _DataError.Add(rd);
                    }
                }
            }
        }
    }
}
