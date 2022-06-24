using M13.InterviewProject.Application.Options;

namespace M13.InterviewProject.Web.Extensions;

public static class FeaturesExtension
{
    public static IServiceCollection SetupFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        var flagsOptions = new FeatureFlagsOptions();
        configuration.GetSection(FeatureFlagsOptions.Section).Bind(flagsOptions);

        if (flagsOptions.UseRedis)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("RedisConnection").Value;
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }
        
        return services;
    }
}