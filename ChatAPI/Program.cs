using ChatAPI;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<EFDBContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await SampleData.InitUsers(userManager);
            }
            host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
