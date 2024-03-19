using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Models.DTO;
using System.Security.Claims;

namespace ScheduleManager.Logics.File
{
    public class ReadFile
    {
        public List<RootData>? _ListRootData { get; set; }
        public List<RootData>? _DataError { get; set; }



        public void Read(string filePath)
        {
            _ListRootData = new List<RootData>();
            _DataError = new List<RootData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    CheckValid cv = new CheckValid(values, _ListRootData);
                    var isValidRecord = cv.Condition();
                    RootData rd = new RootData
                    {
                        Class = values[0],
                        Subject = values[1],
                        Room = values[2],
                        Teacher = values[3],
                        TimeSlot = values[4],
                    };
                    if (isValidRecord)
                    {
                        _ListRootData.Add(rd);
                    }
                    else
                    {
                        _DataError.Add(rd);
                    }
                }
            }
        }
    }
}
