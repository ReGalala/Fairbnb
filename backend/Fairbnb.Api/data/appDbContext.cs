using Fairbnb.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fairbnb.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Unit> Units { get; set;}
}