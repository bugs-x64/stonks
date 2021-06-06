using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StonksCore.Data;

namespace StonksWebApi
{
    public static class WebHostExtensions
    {
        public static async Task RunApplicationAsync(this IHostBuilder builder)
        {
            await builder.Build().RunApplicationAsync();
        }

        public static async Task RunApplicationAsync(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var migrator = scope.ServiceProvider.GetRequiredService<StonksDbContext>();
                await migrator.Database.MigrateAsync();
            }

            await webHost.RunAsync();
        }
    }
}