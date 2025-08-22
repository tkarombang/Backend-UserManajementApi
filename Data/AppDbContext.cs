
using Backend_UserManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_UserManagementApi.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
  }
}