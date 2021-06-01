using LogReport.Bll;
using LogReport.Bll.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;


namespace LogReport.Client
{
    class Program
    {
        static readonly string _filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\access.log";

        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            DefineScope(host.Services);
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services
                    .AddTransient<IReportManager, ReportManager>()
                    .AddTransient<IParser, LogParser>()
                    .AddTransient<ReportLogger>());

        static void DefineScope(IServiceProvider serviceProvider)
        {
            using IServiceScope serviceScope = serviceProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var logger = provider.GetRequiredService<ReportLogger>();

            Console.WriteLine("Welcome!");
            logger.LogReport(_filePath);
        }

    }
}
