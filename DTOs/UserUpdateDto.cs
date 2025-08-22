namespace Backend_UserManagementApi.DTOs
{
  public class UserUpdateDto
  {
    public string Nama { get; set; }
    public string Email { get; set; }
    public string NomorTelepon { get; set; }
    public bool StatusAktif { get; set; }
    public string Departemen { get; set; }

  }
}