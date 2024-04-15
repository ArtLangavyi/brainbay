using Elastic.Apm.SerilogEnricher;

using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace RickAndMorty.Shared.Services;
public static class LogService
{
    private static readonly List<string> NonLocalEnv = ["Prod", "Acc", "Test"];

    public static LoggerConfiguration AddLogger(IConfiguration configuration, string applicationName)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.MachineName)
            .Enrich.WithProperty("Server", Environment.MachineName)
            .Enrich.WithProperty("Application", applicationName);


        if (configuration.GetSection("ElasticApm").GetValue<bool>("Enabled"))
        {
            logger.Enrich.WithElasticApmCorrelationInfo();
        }

        if (NonLocalEnv.Contains(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
        {
            logger.WriteTo.Console(new RenderedCompactJsonFormatter());
        }
        else
        {
            logger.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code);
        }

        return logger;
    }
}
