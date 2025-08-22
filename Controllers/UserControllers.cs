using Backend_UserManagementApi.Data;
using Backend_UserManagementApi.DTOs;
using Backend_UserManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_UserManagementApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
      _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      var userDtos = users.Select(u => new UserReadDto
      {
        Id = u.Id,
        Nama = u.Nama,
        Email = u.Email,
        NomorTelepon = u.NomorTelepon,
        StatusAktif = u.StatusAktif,
        Departemen = u.Departemen
      });
      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound(new { message = "User tidak Ditemukan" });
      }

      var userDto = new UserReadDto
      {
        Id = user.Id,
        Nama = user.Nama,
        Email = user.Email,
        NomorTelepon = user.NomorTelepon,
        StatusAktif = user.StatusAktif,
        Departemen = user.Departemen,
      };

      return Ok(userDto);
    }


    [HttpPost]
    public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto dto)
    {
      var user = new User
      {
        Nama = dto.Nama,
        Email = dto.Email,
        NomorTelepon = dto.NomorTelepon,
        StatusAktif = dto.StatusAktif,
        Departemen = dto.Departemen
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var readDto = new UserReadDto
      {
        Id = user.Id,
        Nama = user.Nama,
        Email = user.Email,
        NomorTelepon = user.NomorTelepon,
        StatusAktif = user.StatusAktif,
        Departemen = user.Departemen
      };

      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, readDto);

    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UserReadDto>> CreateUser(UserUpdateDto dto)
    {
      var user = new User
      {
        Nama = dto.Nama,
        Email = dto.Email,
        NomorTelepon = dto.NomorTelepon,
        StatusAktif = dto.StatusAktif,
        Departemen = dto.Departemen
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var readDto = new UserReadDto
      {
        Id = user.Id,
        Nama = user.Nama,
        Email = user.Email,
        NomorTelepon = user.NomorTelepon,
        StatusAktif = user.StatusAktif,
        Departemen = user.Departemen
      };

      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, readDto);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound(new { message = "User tidak ditemukan." });
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return Ok(new { message = "User berhasil dihapus" });
    }
  }
}