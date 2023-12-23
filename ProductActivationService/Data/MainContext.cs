using ProductActivationService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductActivationService.Data
{
  /// <summary>
  /// MainContext
  /// </summary>
  public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
  {
    public DbSet<CustomerEntity> Customer { get; set; } = null!;
  }
}