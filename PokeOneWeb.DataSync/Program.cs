using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import;

namespace PokeOneWeb.DataSync
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggerFactory => loggerFactory.AddConsole())
                .ConfigureServices((_, services) =>
                {
                    Startup.ConfigureServices(services, configuration);
                })
                .Build();

            using var serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var importService = provider.GetRequiredService<IGoogleSpreadsheetImportService>();
            // var readModelUpdateService = provider.GetRequiredService<IReadModelUpdateService>();

            var importReport = await importService.ImportSpreadsheetData();
            // readModelUpdateService.UpdateReadModel(importReport);
        }
    }
}