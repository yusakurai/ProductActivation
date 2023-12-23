using ProductActivation.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductActivation.Service.Data
{
  /// <summary>
  /// MainContext
  /// </summary>
  public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
  {
    public DbSet<Customer> Customer { get; set; } = null!;
  }
}