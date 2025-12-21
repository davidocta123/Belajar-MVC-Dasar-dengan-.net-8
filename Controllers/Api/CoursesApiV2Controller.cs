using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootcampMvp.Data;
using BootcampMvp.Models;

namespace BootcampMvp.Controllers.Api
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/CoursesApi")]
    // [Route("api/[controller]")]
    // [ApiVersion("2.0")]
    // [Route("api/v{version:apiVersion}/[controller]")]
    // [ApiController]
    public class CoursesApiV2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesApiV2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CoursesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesV2()
        {
            return await _context.Courses.Select(c => new Course
            {
                Id = c.Id,
                CourseName = c.CourseName,
                StudentId = c.StudentId,
                Student = c.Student // Exclude sensitive data or add additional fields as needed
            }).ToListAsync();
        }

        // GET: api/CoursesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/CoursesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CoursesApi
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCourse),
                new { id = course.Id },
                course
            );
        }

        // DELETE: api/CoursesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}