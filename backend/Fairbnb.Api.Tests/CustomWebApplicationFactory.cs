using Fairbnb.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fairbnb.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection;

    public CustomWebApplicationFactory()
    {
        // Keep the connection open so the in-memory database stays alive
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Jwt:Key", "ThisIsATestKeyThatIsDefinitelyLongEnough123!");

        builder.ConfigureServices(services =>
        {
            // Remove all database-related registrations
            var descriptorsToRemove = services
                .Where(d => d.ServiceType.FullName!.Contains("DbContextOptions"))
                .ToList();
            foreach (var d in descriptorsToRemove)
                services.Remove(d);

            // Add SQLite in-memory database for testing
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(_connection));

            // Create the database tables
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        _connection.Close();
        base.Dispose(disposing);
    }
}