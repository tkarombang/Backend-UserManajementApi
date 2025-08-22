using System.ComponentModel.DataAnnotations;

namespace Backend_UserManagementApi.Models
{
  public class User
  {
    public int Id { get; set; } // Primary key

    [Required]
    [MaxLength(100)]
    public string Nama { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(10)]
    [RegularExpression(@"^\d+$", ErrorMessage = "Nomor Telepon hanya boleh Angka.")]
    public string NomorTelepon { get; set; } = string.Empty;

    [Required]
    public bool StatusAktif { get; set; }

    [Required]
    [MaxLength(50)]
    public string Departemen { get; set; } = string.Empty;
  }
}