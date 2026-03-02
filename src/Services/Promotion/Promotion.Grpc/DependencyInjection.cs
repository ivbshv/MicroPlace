namespace Promotion.Grpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddGrpc();
            services.AddGrpcReflection();

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
