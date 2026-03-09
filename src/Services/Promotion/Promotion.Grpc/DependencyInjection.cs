using Promotion.Grpc.Persistence.Extensions;
using Promotion.Grpc.Persistence.Repositories;
using Promotion.Grpc.Services;
using System.Threading.Tasks;

namespace Promotion.Grpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var mySqlConnection = configuration.GetConnectionString("MySqlConnection");

            services.AddScoped<IDbConnection>(_ =>
                new MySqlConnection(mySqlConnection));

            services.AddGrpc();
            services.AddGrpcReflection();

            var assembly = typeof(Program).Assembly;
            var licenseKey = configuration.GetSection("Mediatr:LicenseKey").Value;

            services.AddMediatR(config =>
            {
                config.LicenseKey = licenseKey;
                config.RegisterServicesFromAssemblies(assembly);
                
            });
            services.AddScoped<IPromoRepository, PromoRepository>();

            return services;
        }

        public static async Task<WebApplication> UseApiServices(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
            await DatabaseExtensions.SeedAsync(connection);
            app.MapGrpcReflectionService();

            app.MapGrpcService<PromoGrpcService>();

            return app;
        }
    }
}
