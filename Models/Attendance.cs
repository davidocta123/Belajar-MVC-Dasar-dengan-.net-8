using System.ComponentModel.DataAnnotations;

namespace BootcampMvp.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Siswa wajib dipilih")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Tanggal wajib diisi")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status kehadiran wajib dipilih")]
        [RegularExpression("Hadir|Izin|Alpha", ErrorMessage = "Status harus 'Hadir', 'Izin', atau 'Alpha'")]
        public required string Status { get; set; }

        // Navigation Property (tanpa annotation)
        public Student? Student { get; set; }
    }
}
