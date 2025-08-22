using System.ComponentModel.DataAnnotations;

namespace Backend_UserManagementApi.DTOs
{
  public class UserReadDto
  {
    public int Id { get; set; }
    public required string Nama { get; set; }
    public required string Email { get; set; }
    public required string NomorTelepon { get; set; }
    public required bool StatusAktif { get; set; }
    public required string Departemen { get; set; }
  }
}