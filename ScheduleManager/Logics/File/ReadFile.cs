using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.File
{
    public class ReadFile
    {
        List<RootData> RootDatas { get; set; }
        List<RootData> DataError { get; set; }

        public void Read(string filePath)
        {
            RootDatas = new List<RootData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');









                    if (values.Length > 0 && values.Length < 6)
                    {
                        RootData data = new RootData
                        {
                            Class = values[0],
                            Subject = values[1],
                            Room = values[2],
                            Teacher = values[3],
                            TimeSlot = values[4],
                        };
                        RootDatas.Add(data);
                    }
                }
            }
        }
    }
}
