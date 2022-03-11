using Microsoft.Extensions.DependencyInjection.Extensions;
using MyStore.Core.EntityFrameworkCore.Extensions;
using MyStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using MyStore.Core.ServiceDiscovery.Consul.Extensions;
using MyStore.Core.ServiceDiscovery.Extensions;
using MyStore.Services.Catalog.Repository;
using MyStore.Services.Catalog.Repository.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure service discovery
builder.Services.AddServiceDiscovery(builder.Configuration);
builder.Services.AddConsulDiscovery(builder.Configuration);

// Configure repositories
builder.Services.AddSqlDatabase<CatalogDbContext>(builder.Configuration);
builder.Services.TryAddScoped<IBrandsRepository, BrandsRepository>();
builder.Services.TryAddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.TryAddScoped<IProductsRepository, ProductsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDatabaseAsync<CatalogDbContext>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
