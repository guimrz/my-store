using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Configuration
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();

// Configure ocelot
builder.Services.AddOcelot()
    .AddConsul();

// Configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

// Configure http request pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

await app.UseOcelot();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();
