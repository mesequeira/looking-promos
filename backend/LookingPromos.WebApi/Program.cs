using LookingPromos.Dependencies.Swagger;
using LookingPromos.SharedKernel.Persistence;
using LookingPromos.SharedKernel.Persistence.Abstractions.Contexts;
using LookingPromos.WebApi.Application;
using LookingPromos.WebApi.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddApplication();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", config =>
    {
        config.PermitLimit = 10; // Número máximo de solicitudes permitidas
        config.Window = TimeSpan.FromSeconds(10); // Ventana de tiempo
    });
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Looking.Promos v1"); });

    using var scope = app.Services.CreateScope();

    await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();