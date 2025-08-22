using Backend_UserManagementApi.Data;
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
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound(new { message = "User tidak Ditemukan" });
      }

      return user;
    }


    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
      if (await _context.Users.AnyAsync(u => u.Email == user.Email))
      {
        return BadRequest(new { message = "Email Sudah Ada" });
      }

      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, User user)
    {
      if (id != user.Id)
      {
        return BadRequest(new { message = "ID User Tidak Sesuai" });
      }

      var existingUser = await _context.Users.FindAsync(id);
      if (existingUser == null)
      {
        return NotFound(new { message = "User tidak Ditemukan" });
      }

      existingUser.Nama = user.Nama;
      existingUser.Email = user.Email;
      existingUser.NomorTelepon = user.NomorTelepon;
      existingUser.StatusAktif = user.StatusAktif;
      existingUser.Departemen = user.Departemen;

      await _context.SaveChangesAsync();


      return Ok(existingUser);
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