using System.ComponentModel.DataAnnotations;

namespace Backend_UserManagementApi.DTOs
{
  public class UserCreateDto
  {
    [Required]
    [StringLength(100, ErrorMessage = "Nama Tidak boleh lebih dari 100 karakter")]
    public required string Nama { get; set; }

    [Required(ErrorMessage = "Email harus diisi")]
    [EmailAddress(ErrorMessage = "Email tidak Valid.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Nomor Telepon Harus Diisi")]
    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Nomor Telepon harus berupa angka dan panjang antara 10 hingga 15 karakter.")]
    public required string NomorTelepon { get; set; }

    [Required(ErrorMessage = "Status aktif harus diisi.")]
    public required bool StatusAktif { get; set; }

    [Required(ErrorMessage = "Departemen harus diisi.")]
    [StringLength(50, ErrorMessage = "Departemen tidak boleh lebih dari 50 karakter.")]
    public required string Departemen { get; set; }
  }
}