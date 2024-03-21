using ScheduleManager.Models;
using ScheduleManager.Models.DTO;

namespace ScheduleManager.Logics.InsertToDatabase
{
    public class InsertToDB
    {
        private readonly ScheduleManagerContext _context;

        public InsertToDB(ScheduleManagerContext context)
        {
            _context = context;
        }


    }
}
