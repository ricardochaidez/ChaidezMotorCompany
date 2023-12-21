using Microsoft.AspNetCore.Mvc;

namespace ChaidezMotorCompany.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedisOutputCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisEndpoint = configuration.GetValue<string>("Cache:CarsOutputCache:Endpoint");
        var redisInstanceName = configuration.GetValue<string>("Cache:CarsOutputCache:InstanceName");

        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = redisEndpoint;
            options.InstanceName = redisInstanceName;
        });

        var expirationSeconds = configuration.GetValue<int>("Cache:CarsOutputCache:ExpirationSeconds");
        services.AddOutputCache(options => 
        {
            options.AddPolicy(OutputCachePolicies.CARS,
                new OutputCachePolicy(OutputCachePolicies.CARS,
                duration: TimeSpan.FromSeconds(expirationSeconds)));
        });
        
        return services;
    }

    public static IServiceCollection AddResponseCacheProfiles(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddResponseCaching();
        var expirationSeconds = configuration.GetValue<int>("Cache:CarsOutputCache:ExpirationSeconds");
        services.AddControllers(options =>
        {
            options.CacheProfiles.Add(ResponseCacheProfiles.CARS,
            new CacheProfile()
            {
                Duration = expirationSeconds,
                Location = ResponseCacheLocation.Client,
                NoStore = false
            });
        });
        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var redisEndpoint = configuration.GetValue<string>("Cache:CarsOutputCache:Endpoint");
        var redisInstanceName = configuration.GetValue<string>("Cache:CarsOutputCache:InstanceName");

        services.AddHealthChecks()
            .AddRedis(redisEndpoint, $"Redis:{redisInstanceName}");

        return services;
    }
}
