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

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapGrpcReflectionService();

            app.MapGrpcService<GreeterService>();

            return app;
        }
    }
}
