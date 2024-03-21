using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Models;
using ScheduleManager.Models.DTO;
using System.Security.Claims;

namespace ScheduleManager.Logics.File
{
    public class ReadFile
    {
        private CheckValid _checkValid { get; set; }

        public ReadFile(CheckValid checkValid)
        {
            _checkValid = checkValid;
        }

        public (List<RootDataValid>?, List<RootDataError>?) Read(string filePath)
        {
            List<RootDataValid>? listRootData = new List<RootDataValid>();
            List<RootDataError>? listDataError = new List<RootDataError>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    var result = _checkValid.Condition(values, listRootData);

                    if (result.Item1)
                    {
                        RootDataValid rd = new RootDataValid
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
                        RootDataError? rd = null;
                        if (result.Item3)
                        {
                            rd = new RootDataError
                            {
                                MissData = string.Join(",", values),
                                Message = result.Item2 ?? "loi roi"
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
                                Message = result.Item2 ?? "loi roi"
                            };
                        }
                        listDataError.Add(rd);
                    }
                }











                return (listRootData, listDataError);
            }
        }
    }
}
