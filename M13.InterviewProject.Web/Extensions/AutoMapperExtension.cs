using M13.InterviewProject.Application.MappingProfiles;

namespace M13.InterviewProject.Web.Extensions;

public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(SpellCheckerProfile));
        
        return services;
    }
}