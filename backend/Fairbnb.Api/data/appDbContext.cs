using Microsoft.EntityFrameworkCore;
namespace Fairbnb.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}