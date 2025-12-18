using Microsoft.AspNetCore.Mvc;
using BootcampMvp.Models;
using BootcampMvp.Services;
namespace BootcampMvp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        // Dependency Injection melalui konstruktor
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: Student
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();
            return View(students);
        }


        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.AddStudent(student);
                return RedirectToAction(nameof(Index)); // Redirect ke daftar siswa
            }
            return View(student);
        }
        // GET: Student/Edit/{id}
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        // POST: Student/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);

        }
        // GET: Student/Delete/{id}
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        // POST: Student/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: Student/Details/{id}
        public IActionResult Details(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}