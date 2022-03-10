using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot().AddConsul();


var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();
