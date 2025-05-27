using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<OMDbContext>(options =>
                        options.UseSqlServer("Server=MAN\\SQLEXPRESS;Database=OM_Database;integrated security=True;TrustServerCertificate=True;"));
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OMDbContext>();

            // Ensure DB and seed sample
            context.Database.Migrate(); // Applies migrations

            Console.WriteLine("Database created and migrated.");
        }
    }
}
