namespace Backend_UserManagementApi.DTOs
{
  public class UserReadDto
  {
    public int Id { get; set; }
    public string Nama { get; set; }
    public string Email { get; set; }
    public string NomorTelepon { get; set; }
    public bool StatusAktif { get; set; }
    public string Departemen { get; set; }
  }
}