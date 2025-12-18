using BootcampMvp.Models;

namespace BootcampMvp.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IStudentService _studentService;

        private static List<Attendance> _attendances = new();
        private static int _counter = 1;

        public AttendanceService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IEnumerable<Attendance> GetAll()
        {
            foreach (var a in _attendances)
            {
                a.Student = _studentService.GetStudentById(a.StudentId);
            }
            return _attendances;
        }

        public Attendance? GetById(int id)
        {
            var attendance = _attendances.FirstOrDefault(a => a.Id == id);
            if (attendance != null)
            {
                attendance.Student = _studentService.GetStudentById(attendance.StudentId);
            }
            return attendance;
        }

        public void Add(Attendance attendance)
        {
            attendance.Id = _counter++;
            _attendances.Add(attendance);
        }

        public void Update(Attendance attendance)
        {
            var existing = GetById(attendance.Id);
            if (existing != null)
            {
                existing.StudentId = attendance.StudentId;
                existing.Date = attendance.Date;
                existing.Status = attendance.Status;
            }
        }

        public void Delete(int id)
        {
            var attendance = GetById(id);
            if (attendance != null)
            {
                _attendances.Remove(attendance);
            }
        }
    }
}
