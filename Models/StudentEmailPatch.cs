using System.ComponentModel.DataAnnotations;

public class StudentEmailPatch
{
    [Required(ErrorMessage = "Email wajib diisi")]
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    public string Email { get; set; } = string.Empty;
}
