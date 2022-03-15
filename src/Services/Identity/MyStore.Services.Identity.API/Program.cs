using MyStore.Services.Identity.API;
using MyStore.Services.Identity.Repository;
using Serilog;
using MyStore.Core.EntityFrameworkCore.Extensions;
using MyStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using MyStore.Services.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.DbContexts;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddRazorPages();

    builder.Services.AddSqlDatabase<IdentityDbContext>(builder.Configuration);

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();

    builder.Services
        .AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;

            // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
            options.EmitStaticAudienceClaim = true;
            options.IssuerUri = "http://identity_service";
        })
        .AddConfigurationStore(options => 
        {
            options.ConfigureDbContext = configuration => configuration.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql 
                => sql.MigrationsAssembly("MyStore.Services.Identity.Repository"));
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = configuration => configuration.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql
                => sql.MigrationsAssembly("MyStore.Services.Identity.Repository"));
        })
        .AddAspNetIdentity<ApplicationUser>();

    //builder.Services.AddAuthentication()
    //    .AddGoogle(options =>
    //    {
    //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    //    // register your IdentityServer with Google at https://console.developers.google.com
    //    // enable the Google+ API
    //    // set the redirect URI to https://localhost:5001/signin-google
    //    options.ClientId = "copy client ID from Google here";
    //        options.ClientSecret = "copy client secret from Google here";
    //    });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseIdentityServer();
    app.UseAuthorization();

    app.MapRazorPages()
        .RequireAuthorization();    

    await app.MigrateDatabaseAsync<IdentityDbContext>();
    await app.MigrateDatabaseAsync<ConfigurationDbContext>();
    await app.MigrateDatabaseAsync<PersistedGrantDbContext>();
    
    await SeedData.EnsureSeedDataAsync(app);

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}