using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure logger
    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    // Configure services
    builder.Configuration
        .AddJsonFile("ocelot.json")
        .AddJsonFile("ocelot.catalog.json")
        .AddEnvironmentVariables();

    builder.Services.AddOcelot()
        .AddConsul();

    var app = builder.Build();

    // Configure serilog for requests
    app.UseSerilogRequestLogging();

    // Configure http request pipeline
    await app.UseOcelot();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown completed");
    Log.CloseAndFlush();
}