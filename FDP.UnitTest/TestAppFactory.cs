using FDP.Infrastructure;
using FDP.Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FDP.UnitTest;

public class TestAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove existing SQL Server DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<FdpContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add in-memory test database
            services.AddDbContext<FdpContext>(options =>
                options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid()));
                //options.UseInMemoryDatabase("TestDb_"));

            // Build service provider and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FdpContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Seed(db);
        });
    }

    public static void Seed(FdpContext context)
    {
        context.Countries.RemoveRange(context.Countries);
        context.States.RemoveRange(context.States);
        context.Users.RemoveRange(context.Users);
        context.Addresses.RemoveRange(context.Addresses);

        var countries = FakeCountryData.GetFakeCountryData();
        context.Countries.AddRange(countries);
        context.SaveChanges();

        var states = FakeStateData.GetStateFakeData();
        context.States.AddRange(states);
        context.SaveChanges();

        var users = FakeUserData.GetUsersFakeData();
        context.Users.AddRange(users);
        context.SaveChanges();

        var addresses = FakeAddressData.GetAddressFakeData();
        context.Addresses.AddRange(addresses);
        context.SaveChanges();
    }
}
