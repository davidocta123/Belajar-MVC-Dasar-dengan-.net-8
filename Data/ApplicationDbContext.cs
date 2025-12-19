using Microsoft.EntityFrameworkCore;
using BootcampMvp.Models;
namespace BootcampMvp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Student> Students { get; set; } // Merepresentasikan tabel Students
                                                     // Anda bisa menambahkan konfigurasi model tambahan di sini jika diperlukan
        public DbSet<Attendance> Attendances { get; set; } // Merepresentasikan tabel Attendances
        public DbSet<Course> Courses { get; set; } // Merepresentasikan tabel Course
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Contoh: Mengatur nama tabel secara eksplisit
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Attendance>().ToTable("Attendances");
            modelBuilder.Entity<Course>().ToTable("Courses");

            // Relasi One-to-Many: Student -> Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed Data
            //     modelBuilder.Entity<Student>().HasData(
            //        new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 20 },
            //        new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Age = 22 }
            //    );

            //     modelBuilder.Entity<Course>().HasData(
            //         new Course { Id = 1, CourseName = "Matematika", StudentId = 1 },
            //         new Course { Id = 2, CourseName = "Fisika", StudentId = 1 },
            //         new Course { Id = 3, CourseName = "Kimia", StudentId = 2 }
            //     );
        }
    }
}