using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;
using ScheduleManager.Utils.Enum;

namespace ScheduleManager.Pages.Timetable
{
    public class DetailModel : PageModel
    {
        private readonly ScheduleManagerContext _context;

        public Schedule slot1 { get; set; }
        public SelectList classes1 { get; set; }
        public SelectList subjects1 { get; set; }
        public SelectList teachers1 { get; set; }
        public SelectList slots1 { get; set; }
        public SelectList rooms1 { get; set; }
        public SelectList timeSlots { get; set; }

        public Schedule slot2 { get; set; }
        public SelectList classes2 { get; set; }
        public SelectList subjects2 { get; set; }
        public SelectList teachers2 { get; set; }
        public SelectList slots2 { get; set; }
        public SelectList rooms2 { get; set; }

        public DetailModel(ScheduleManagerContext context)
        {
            _context = context;
        }

        public void OnGet(int slotId)
        {
            slot1 = _context.Schedules.Include(s => s.TimeSlot).FirstOrDefault(s => s.Id == slotId);
            slot2 = _context.Schedules.Include(s => s.TimeSlot).FirstOrDefault(s => s.ClassId == slot1.ClassId && s.SubjectId == slot1.SubjectId && s.TeacherId == slot1.TeacherId && s.RoomId == slot1.RoomId && s.TimeSlotId != slot1.TimeSlotId);
            var listClass = _context.GroupClasses.ToList();
            var listSubject = _context.Subjects.ToList();
            var listTeacher = _context.Teachers.ToList();
            var listRoom = _context.Rooms.ToList();
            var listTimeSlot = Enum.GetValues(typeof(TimeSlot)).Cast<TimeSlot>().ToList();

            classes1 = new SelectList(listClass, nameof(GroupClass.Id), nameof(GroupClass.Name), slot1.ClassId);
            subjects1 = new SelectList(listSubject, nameof(Models.Subject.Id), nameof(Models.Subject.Name), slot1.SubjectId);
            teachers1 = new SelectList(listTeacher, nameof(Models.Teacher.Id), nameof(Models.Teacher.Name), slot1.TeacherId);
            rooms1 = new SelectList(listRoom, nameof(Models.Room.Id), nameof(Models.Room.Name), slot1.RoomId);

            classes2 = new SelectList(listClass, nameof(GroupClass.Id), nameof(GroupClass.Name), slot2.ClassId);
            subjects2 = new SelectList(listSubject, nameof(Models.Subject.Id), nameof(Models.Subject.Name), slot2.SubjectId);
            teachers2 = new SelectList(listTeacher, nameof(Models.Teacher.Id), nameof(Models.Teacher.Name), slot2.TeacherId);
            rooms2 = new SelectList(listRoom, nameof(Models.Room.Id), nameof(Models.Room.Name), slot2.RoomId);

            var timeslotConvert = ConvertTimeslot(int.Parse(slot1.TimeSlot.Slot1), slot1.Date, int.Parse(slot2.TimeSlot.Slot1), slot2.Date);
            TimeSlot timeSlotId;
            Enum.TryParse(timeslotConvert, out timeSlotId);
            timeSlots = new SelectList(GetListTimeSlot(listTimeSlot), nameof(Models.DTO.TimeSlot.Id), nameof(Models.DTO.TimeSlot.Name), (int)timeSlotId);
        }

        private List<Models.DTO.TimeSlot> GetListTimeSlot(List<TimeSlot> listTimeSlot)
        {
            List<Models.DTO.TimeSlot> listTimeSlotClone = new List<Models.DTO.TimeSlot>();

            for (int i = 0; i < listTimeSlot.Count; i++)
            {
                Models.DTO.TimeSlot ts = new Models.DTO.TimeSlot
                {
                    Id = (int)listTimeSlot[i],
                    Name = listTimeSlot[i].ToString(),
                };
                listTimeSlotClone.Add(ts);
            }

            return listTimeSlotClone;
        }

        private string ConvertTimeslot(int slot1, DateTime date1, int slot2, DateTime date2)
        {
            string result = "";
            if ((slot1 == 1 && slot2 == 2) || (slot1 == 2 && slot2 == 1))
            {
                result = result + "A";
                if (slot1 == 1 && slot2 == 2)
                {
                    int value1 = (int)date1.DayOfWeek + 1;
                    int value2 = (int)date2.DayOfWeek + 1;
                    result = result + value1.ToString() + value2.ToString();
                }
                else
                {
                    int value1 = (int)date1.DayOfWeek + 1;
                    int value2 = (int)date2.DayOfWeek + 1;
                    result = result + value2.ToString() + value1.ToString();
                }
            }
            else
            {
                result = result + "P";
                if (slot1 == 3 && slot2 == 4)
                {
                    int value1 = (int)date1.DayOfWeek + 1;
                    int value2 = (int)date2.DayOfWeek + 1;
                    result = result + value1.ToString() + value2.ToString();
                }
                else
                {
                    int value1 = (int)date1.DayOfWeek + 1;
                    int value2 = (int)date2.DayOfWeek + 1;
                    result = result + value2.ToString() + value1.ToString();
                }
            }
            return result;
        }
    }
}
