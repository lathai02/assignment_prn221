using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.File
{
    public class ReadFile
    {
        List<RootData> RootDatas {  get; set; }
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








                    foreach (string value in values)
                    {
                        RootData data = new RootData
                        {
                            Class = value,
                            Subject = value,
                            Room = value,
                            Teacher = value,
                            TimeSlot = value,
                        };

                        RootDatas.Add(data);
                    }
                }
            }
        }
    }
}
