using Catalog.Application.Queries.BrandQueries;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API;


public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var licenseKey = configuration.GetSection("Mediatr:LicenseKey").Value;
        var assembly = typeof(GetBrandsQuery).Assembly;
        services.AddMediatR(config =>
        {
            config.LicenseKey = licenseKey;
            config.RegisterServicesFromAssemblies( assembly );
        });
        return services;
    }

    public static WebApplication UseApiServices(
        this WebApplication app
    )
    {
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapGet("/", () => "Hello World!");

        return app;
    }
}