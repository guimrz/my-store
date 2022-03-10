using Ocelot;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();


var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();
