using LookingPromos.SharedKernel.Persistence;
using LookingPromos.Worker;
using LookingPromos.Worker.Application;
using LookingPromos.Worker.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();