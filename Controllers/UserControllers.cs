using Backend_UserManagementApi.Data;
using Backend_UserManagementApi.DTOs;
using Backend_UserManagementApi.Models;
using Backend_UserManagementApi.Models.Responses;
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
      if (users == null || !users.Any())
      {
        return NotFound(new ErrorResponse
        {
          StatusCode = 404,
          Message = "Tidak ada Pengguna yang ditemukan",
          Errors = new List<string> { "DATA PENGGUNA KOSONG" }
        });
      }


      var userDtos = users.Select(u => new UserReadDto
      {
        Id = u.Id,
        Nama = u.Nama,
        Email = u.Email,
        NomorTelepon = u.NomorTelepon,
        StatusAktif = u.StatusAktif,
        Departemen = u.Departemen
      }).ToList();
      
      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        var errorResponse = new ErrorResponse
        {
          StatusCode = 404,
          Message = "User Tidak Ditemukan"
        };
        return NotFound(errorResponse);
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
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
        var errorResponse = new ErrorResponse
        {
          StatusCode = 400,
          Message = "Validasi gagal",
          Errors = errors
        };
        return BadRequest(errorResponse);
      }


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
    public async Task<ActionResult<UserReadDto>> UpdateUser(int id, UserUpdateDto dto)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
        var errorResponse = new ErrorResponse
        {
          StatusCode = 400,
          Message = "Validasi gagal",
          Errors = errors
        };
        return BadRequest(errorResponse);
      }

      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound(new ErrorResponse
        {
          StatusCode = 404,
          Message = "User Tidak Ditemukan",
          Errors = new List<string> {"ID Pengguna Tidak ditemukan"}
        });
      }

      user.Nama = dto.Nama;
      user.Email = dto.Email;
      user.NomorTelepon = dto.NomorTelepon;
      user.StatusAktif = dto.StatusAktif;
      user.Departemen = dto.Departemen;

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

      return Ok(readDto);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound(new ErrorResponse
        {
          StatusCode = 404,
          Message = "User Tidak Ditemukan",
          Errors = new List<string> {"ID Pengguna tidak ditemukan"}
        });
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return Ok(new { message = "User berhasil dihapus" });
    }
  }
}