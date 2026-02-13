using Catalog.API;
using Catalog.Infrastructure;
using Catalog.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

app.Run();
