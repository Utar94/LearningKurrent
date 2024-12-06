using LearningKurrent.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningKurrent.Infrastructure;

internal class CommerceContext : DbContext
{
  public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
  {
  }

  public DbSet<ProductEntity> Products => Set<ProductEntity>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
