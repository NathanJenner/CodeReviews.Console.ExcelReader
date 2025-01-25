using ExcelReader.NathanJenner.Repository;
using ExcelReader.NathanJenner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace ExcelReader.NathanJenner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = CreateServices();

            Application app = services.GetRequiredService<Application>();

            app.Run();
        }

        private static ServiceProvider CreateServices()
        {
            var serviceCollection = new ServiceCollection();

            IConfiguration configuration = GetConfiguration();

            serviceCollection.AddSingleton<Application>();
            //serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 5)
    ));
            serviceCollection.AddSingleton<PlayerRepository>();
            serviceCollection.AddSingleton<PlayerServices>();

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }

    public class Application(ApplicationDbContext _dbContext, PlayerServices _playerServices)
    {
        public void Run()
        {
            AnsiConsole.Markup("\n\n\n[fuchsia]Deleting the database.[/]\n");
            _dbContext.Database.EnsureDeleted();

            AnsiConsole.Markup("\n\n\n[fuchsia]Creating the database.[/]\n");
            _dbContext.Database.EnsureCreated();

            var exportInformation = ExportData.ExportFromExcel();

            _playerServices.PlayerExcelToDbService(exportInformation);

            _playerServices.DisplayEntities();
        }
    }
}