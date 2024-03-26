using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;
using ScheduleManager.Utils.Enum;

namespace ScheduleManager.Pages.Timetable
{
    public class DeleteModel : PageModel
    {
        private readonly ScheduleManagerContext _context;

        public Schedule slot1 { get; set; }
        public Schedule slot2 { get; set; }
        public SelectList classes { get; set; }
        public SelectList subjects { get; set; }
        public SelectList teachers { get; set; }
        public SelectList slots { get; set; }
        public SelectList rooms { get; set; }
        public SelectList timeSlots { get; set; }

        [BindProperty]
        public int slotId1 { get; set; }
        [BindProperty]
        public int slotId2 { get; set; }
        [BindProperty]
        public string classSelected { get; set; }
        [BindProperty]
        public string subjectSelected { get; set; }
        [BindProperty]
        public string teacherSelected { get; set; }
        [BindProperty]
        public string roomSelected { get; set; }
        [BindProperty]
        public string timeslotSelected { get; set; }

        public DeleteModel(ScheduleManagerContext context)
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

            classes = new SelectList(listClass, nameof(GroupClass.Name), nameof(GroupClass.Name), slot1.Class.Name);
            subjects = new SelectList(listSubject, nameof(Models.Subject.Name), nameof(Models.Subject.Name), slot1.Subject.Name);
            teachers = new SelectList(listTeacher, nameof(Models.Teacher.Name), nameof(Models.Teacher.Name), slot1.Teacher.Name);
            rooms = new SelectList(listRoom, nameof(Models.Room.Name), nameof(Models.Room.Name), slot1.Room.Name);

            var timeslotConvert = ConvertTimeslot(int.Parse(slot1.TimeSlot.Slot1), slot1.Date, int.Parse(slot2.TimeSlot.Slot1), slot2.Date);
            TimeSlot timeSlot;
            Enum.TryParse(timeslotConvert, out timeSlot);
            timeSlots = new SelectList(GetListTimeSlot(listTimeSlot), nameof(Models.DTO.TimeSlot.Name), nameof(Models.DTO.TimeSlot.Name), timeSlot);
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

        public IActionResult OnPostDelete()
        {
            var slot1Delete = _context.Schedules.FirstOrDefault(s => s.Id == slotId1);
            var slot2Delete = _context.Schedules.FirstOrDefault(s => s.Id == slotId2);
            var listToDelete = _context.Schedules.Where(s => (s.ClassId == slot1Delete.ClassId && s.SubjectId == slot1Delete.SubjectId && s.TeacherId == slot1Delete.TeacherId && s.RoomId == slot1Delete.RoomId) && (s.TimeSlotId == slot1Delete.TimeSlotId || s.TimeSlotId == slot2Delete.TimeSlotId)).ToList();
            _context.Schedules.RemoveRange(listToDelete);

            var rootDataDelete = _context.RootDataValids.FirstOrDefault(r => r.Class.Equals(classSelected) && r.Subject.Equals(subjectSelected) && r.Teacher.Equals(teacherSelected) && r.Room.Equals(roomSelected) && r.TimeSlot.Equals(timeslotSelected));
            _context.RootDataValids.Remove(rootDataDelete);
            _context.SaveChanges();

            return RedirectToPage("/Timetable/Schedule");
        }
    }
}
