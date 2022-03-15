using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using MyStore.Core.EntityFrameworkCore.Extensions;
using MyStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using MyStore.Core.Mvc.Extensions;
using MyStore.Core.Mvc.Middlewares;
using MyStore.Core.ServiceDiscovery.Consul.Extensions;
using MyStore.Core.ServiceDiscovery.Extensions;
using MyStore.Services.Catalog.Repository;
using MyStore.Services.Catalog.Repository.Abstractions;
using Serilog;
using System.Reflection;

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

    builder.Services.AddControllers();

    // Configure swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => 
    {
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
    });

    // Configure service discovery
    builder.Services.AddServiceDiscovery(builder.Configuration);
    builder.Services.AddConsulDiscovery(builder.Configuration);

    // Configure repositories
    builder.Services.AddSqlDatabase<CatalogDbContext>(builder.Configuration);
    builder.Services.TryAddScoped<IBrandsRepository, BrandsRepository>();
    builder.Services.TryAddScoped<ICategoriesRepository, CategoriesRepository>();
    builder.Services.TryAddScoped<IProductsRepository, ProductsRepository>();

    // Configure mapping
    builder.Services.AddAutoMapper(config => config.AddMaps(Assembly.Load("MyStore.Services.Catalog.Application")));

    // Configure MediatR
    builder.Services.AddMediatR(Assembly.Load("MyStore.Services.Catalog.Application"));

    // Configure request validation
    builder.Services.AddRequestValidation(Assembly.Load("MyStore.Services.Catalog.Application"));

    // Configure health checks
    builder.Services.AddHealthChecks();

    // Configure authentication
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "http://identity_service";
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters.ValidateAudience = false;
            options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

        });

    // Configure authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "catalog");
        });
    });

    var app = builder.Build();

    // Configure serilog for requests
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.MigrateDatabaseAsync<CatalogDbContext>();

    // Configure middlewares
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    // Configure health checks
    app.UseHealthChecks("/health/status");

    app.MapControllers();

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