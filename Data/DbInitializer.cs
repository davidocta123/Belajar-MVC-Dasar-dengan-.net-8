using BootcampMvp.Models;
using System.Linq;

namespace BootcampMvp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Pastikan database sudah dibuat
            context.Database.EnsureCreated();

            // Jika sudah ada data, jangan seed lagi
            if (context.Students.Any())
            {
                return; // DB sudah diisi
            }

            // ===============================
            // Seed Student
            // ===============================
            var students = new Student[]
            {
                new Student { Name = "Alice", Email = "alice@example.com", Age = 20 },
                new Student { Name = "Bob", Email = "bob@example.com", Age = 22 },
                new Student { Name = "Charlie", Email = "charlie@example.com", Age = 21 }
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // ===============================
            // Seed Course
            // ===============================
            var courses = new Course[]
            {
                new Course { CourseName = "Matematika", StudentId = students[0].Id },
                new Course { CourseName = "Fisika", StudentId = students[0].Id },
                new Course { CourseName = "Kimia", StudentId = students[1].Id }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}
