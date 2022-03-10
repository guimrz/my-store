using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Configuration
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();

builder.Services.AddOcelot()
    .AddConsul();

var app = builder.Build();

// Configure http request pipeline
await app.UseOcelot();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();
