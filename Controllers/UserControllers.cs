using AutoMapper;
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
    private readonly IMapper _mapper;

    public UserController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
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

      var userDtos = _mapper.Map<List<UserReadDto>>(users);
      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound(new ErrorResponse
        {
          StatusCode = 404,
          Message = "User yang Kamu Cari Tidak Ada",
          Errors = ["cari ID Yang lain"]
        });
      }

      var userDto = _mapper.Map<UserReadDto>(user);
      return Ok(userDto);
    }


    [HttpPost]
    public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto dto)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return BadRequest(new ErrorResponse
        {
          StatusCode = 400,
          Message = "Validasi gagal",
          Errors = errors
        });
      }

      if (_context.Users.Any(e => e.Email == dto.Email))
      {
        return BadRequest(new ErrorResponse
        {
          StatusCode = 400,
          Message = "Email Sudah Terdaftar",
          Errors = ["Pakai Email yang lain"]
        });
      }

      var user = _mapper.Map<User>(dto);
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var readDto = _mapper.Map<UserReadDto>(user);
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, readDto);

    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UserReadDto>> UpdateUser(int id, UserUpdateDto dto)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound(new ErrorResponse
        {
          StatusCode = 404,
          Message = "Tidak ada Pengguna Yang ingin diUbah",
          Errors = ["ID Pengguna Tidak ditemukan"]
        });
      }

      if (_context.Users.Any(u => u.Email == dto.Email && u.Id != id))
      {
        return BadRequest(new ErrorResponse
        {
          StatusCode = 400,
          Message = "Validasi Gagal",
          Errors = ["Email sudah terdaftar.."]
        });
      }

      _mapper.Map(dto, user);

      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values
          .SelectMany(v => v.Errors)
          .Select(e => e.ErrorMessage)
          .ToList();

        return BadRequest(new ErrorResponse
        {
          StatusCode = 400,
          Message = "Validasi gagal",
          Errors = errors
        });
      }


      _context.Users.Update(user);
      await _context.SaveChangesAsync();


      var readDto = _mapper.Map<UserReadDto>(user);
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
          Message = "Kamu Menghapus Pengguna yang tidak ada",
          Errors = ["ID Pengguna tidak ditemukan"]
        });
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return Ok(new { message = "User berhasil dihapus" });
    }
  }
}