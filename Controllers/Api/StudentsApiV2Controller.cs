using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootcampMvp.Data;
using BootcampMvp.Models;

namespace BootcampMvp.Controllers.Api
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/StudentsApi")]
    // [Route("api/[controller]")]
    // [ApiVersion("2.0")]
    // [Route("api/v{version:apiVersion}/[controller]")]
    // [ApiController]
    public class StudentsApiV2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsApiV2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsV2()
        {
            return await _context.Students.Select(s => new Student
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email           // Exclude sensitive data or add additional fields as needed
            }).ToListAsync();
        }

        // GET: api/StudentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/StudentsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204
        }

        // PATCH: api/v2/StudentsApi/5/email
    [HttpPatch("{id}/email")]
    public async Task<IActionResult> PatchStudentEmailV2(int id, [FromBody] StudentEmailPatch studentEmailPatch)
    {

        var existingStudent = await _context.Students.FindAsync(id);
        if (existingStudent == null)
        {
            return NotFound();
        }

        // Validasi email
        if (string.IsNullOrWhiteSpace(studentEmailPatch.Email)) // Validasi email tidak boleh kosong
        {
            return BadRequest("Email tidak boleh kosong");
        }

        // Perbedaan V2
        existingStudent.Email = studentEmailPatch.Email.ToLower(); // Simpan email dalam huruf kecil

        await _context.SaveChangesAsync(); // Simpan perubahan

        // V2 boleh return data (beda dari V1)
        return Ok(existingStudent); // 200 dengan data student yang diperbarui
    }

        // POST: api/StudentsApi
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student
            );
        }

        // DELETE: api/StudentsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
