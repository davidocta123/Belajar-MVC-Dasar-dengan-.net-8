using Microsoft.AspNetCore.Mvc;
using BootcampMvp.Models;
using BootcampMvp.Services;

namespace BootcampMvp.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IStudentService _studentService;

        public AttendanceController(
            IAttendanceService attendanceService,
            IStudentService studentService)
        {
            _attendanceService = attendanceService;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View(_attendanceService.GetAll());
        }

        public IActionResult Create()
        {
            ViewBag.Students = _studentService.GetAllStudents();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Students = _studentService.GetAllStudents();
                return View(attendance);
            }

            _attendanceService.Add(attendance);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var attendance = _attendanceService.GetById(id);
            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        public IActionResult Edit(int id)
        {
            var attendance = _attendanceService.GetById(id);
            if (attendance == null) return NotFound();

            ViewBag.Students = _studentService.GetAllStudents();
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Students = _studentService.GetAllStudents();
                return View(attendance);
            }

            _attendanceService.Update(attendance);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var attendance = _attendanceService.GetById(id);
            if (attendance == null) return NotFound();

            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _attendanceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
