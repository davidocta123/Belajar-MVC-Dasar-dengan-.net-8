using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BootcampMvp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Mata Kuliah harus diisi.")]
        [StringLength(100, ErrorMessage = "Nama Mata Kuliah tidak lebih dari 100 karakter.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Harus memilih student")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        
        public Student Student { get; set; }
    }
}