using ProductActivationService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductActivationService.Data
{
  /// <summary>
  /// MainContext
  /// </summary>
  public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
  {
    public DbSet<Customer> Customer { get; set; } = null!;
  }
}