
using BurberDinner.Domain.MenuAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BurberDinner.Infrastructure.Persistence
{
  public class BurberDinnerDbContext : DbContext
  {
  public BurberDinnerDbContext(DbContextOptions<BurberDinnerDbContext> options) : base(options)
  {
  }

    public DbSet<Menu> Menus { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

 

}
}