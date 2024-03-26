using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Logics.InsertToDatabase;
using ScheduleManager.Models;
using ScheduleManager.Utils.Enum;

namespace ScheduleManager.Pages.Timetable
{
    public class UpdateModel : PageModel
    {
        private readonly ScheduleManagerContext _context;

        private readonly InsertToDB _insertToDb;
        public Schedule slot1 { get; set; }
        public Schedule slot2 { get; set; }
        public SelectList classes { get; set; }
        public SelectList subjects { get; set; }
        public SelectList teachers { get; set; }
        public SelectList slots { get; set; }
        public SelectList rooms { get; set; }
        public SelectList timeSlots { get; set; }

        public RootDataValid rootDataIdCurrent { get; set; }

        public string? message { get; set; } = null;

        [BindProperty]
        public int slotId1 { get; set; }
        [BindProperty]
        public int slotId2 { get; set; }
        [BindProperty]
        public int rootDataIdCurrentId { get; set; }
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

        private readonly CheckValid _checkValid;
        public UpdateModel(ScheduleManagerContext context, CheckValid checkValid, InsertToDB insertToDb)
        {
            _context = context;
            _checkValid = checkValid;
            _insertToDb = insertToDb;
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

            rootDataIdCurrent = _context.RootDataValids.FirstOrDefault(r => r.Class.Equals(slot1.Class.Name) && r.Subject.Equals(slot1.Subject.Name) && r.Teacher.Equals(slot1.Teacher.Name) && r.Room.Equals(slot1.Room.Name) && r.TimeSlot.Equals(timeSlot.ToString()));
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

        public IActionResult OnPostUpdate()
        {
            var listRootDataValid = _context.RootDataValids.ToList();
            var rootDataDelete = listRootDataValid.FirstOrDefault(r => r.Id == rootDataIdCurrentId);
            listRootDataValid.Remove(rootDataDelete);
            int slotIdCurrent = slotId1;
            if (classSelected != null && subjectSelected != null && roomSelected != null && teacherSelected != null && timeslotSelected != null)
            {
                string[] currentRecord = new string[5];
                currentRecord[0] = classSelected;
                currentRecord[1] = subjectSelected;
                currentRecord[2] = roomSelected;
                currentRecord[3] = teacherSelected;
                currentRecord[4] = timeslotSelected;

                var result = _checkValid.Condition(currentRecord, listRootDataValid);

                if (result.Item1)
                {
                    message = "Update successful";
                    _context.RootDataValids.Remove(rootDataDelete);

                    RootDataValid rdv = new RootDataValid
                    {
                        Class = classSelected,
                        Subject = subjectSelected,
                        Room = roomSelected,
                        Teacher = teacherSelected,
                        TimeSlot = timeslotSelected,
                    };
                    _context.RootDataValids.Add(rdv);

                    var autoGenScheduleData = _context.AutoGenScheduleData.ToList();
                    var ags = autoGenScheduleData.First();
                    var listData = _context.Schedules.ToList();
                    _context.Schedules.RemoveRange(listData);
                    _context.SaveChanges();

                    _insertToDb.InsertData(ags.NumberOfWeek ?? 10, ags.StartDate ?? DateTime.Now);

                    return RedirectToPage("/Timetable/Schedule");
                }
                else
                {
                    message = result.Item2;
                }
            }
            else
            {
                message = "Data not enough";
            }
            return RedirectToPage("/Timetable/Update", new { slotId = slotIdCurrent });
        }
    }
}
