using Basket.API.Infrastructure;
using Basket.API.Models;
using Carter;
using Common.Kernel.Behaviors;
using Common.Kernel.Exceptions.Handler;
using FluentValidation;
using Marten;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration configuration 
        )
        {

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddProblemDetails();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCarter();

            var assembly = typeof(Program).Assembly;
            var licenseKey = configuration.GetSection("Mediatr:LicenseKey").Value;

            services.AddMediatR(config =>
            {
                config.LicenseKey = licenseKey;
                config.RegisterServicesFromAssemblies(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);

            var connectionString = configuration.GetConnectionString("pgConnection")!;

            services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.Schema.For<ShoppingCart>().Identity(x => x.AccountName);
            }).UseLightweightSessions();

            var redisConnectionString = configuration.GetConnectionString("RedisConnection");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "Cart";
            });

            //services.AddScoped<CartRepository>();
            //services.AddScoped<ICartRepository>(provider =>

            //    new RedisCartCacheRepository(
            //        provider.GetRequiredService<CartRepository>(),
            //        provider.GetRequiredService<IDistributedCache>()
            //    )
            //);
            services.AddScoped<ICartRepository, CartRepository>();
            services.Decorate<ICartRepository, RedisCartCacheRepository>();

            return services;
        }

        public static WebApplication UseApiServices(
            this WebApplication app
        )
        {
            app.UseExceptionHandler();
            app.MapGet("/", () => Results.Redirect("/swagger"));
            app.MapCarter();
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
