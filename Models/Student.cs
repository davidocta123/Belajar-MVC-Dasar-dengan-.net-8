using System.ComponentModel.DataAnnotations;
namespace BootcampMvp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nama harus diisi.")]
        [StringLength(100, ErrorMessage = "Nama tidak boleh lebih dari 100 karakter.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email harus diisi.")]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        public string Email { get; set; } = string.Empty;
        [Range(18, 60, ErrorMessage = "Usia harus antara 18 dan 60.")]

        public int Age { get; set; }
    }
}