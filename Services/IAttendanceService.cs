using BootcampMvp.Models;

namespace BootcampMvp.Services
{
    public interface IAttendanceService
    {
        IEnumerable<Attendance> GetAll();
        Attendance? GetById(int id);
        void Add(Attendance attendance);
        void Update(Attendance attendance);
        void Delete(int id);
    }
}
