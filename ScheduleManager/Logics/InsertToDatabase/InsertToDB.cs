using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.InsertToDatabase
{
    public class InsertToDB
    {
        public List<RootDataValid>? _ListRootData { get; set; }


        public InsertToDB(List<RootDataValid>? ListRootData) {
            _ListRootData = ListRootData;
        }



    }
}
