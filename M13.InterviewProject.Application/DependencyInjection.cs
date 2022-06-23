using M13.InterviewProject.Application.Models;
using M13.InterviewProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace M13.InterviewProject.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHttpParseService, HttpParseService>();
        services.AddScoped<IRuleService, RuleService>();
        services.AddScoped<ISpellCheckerService, SpellCheckerService>();
        
        services.AddHttpClients();
        
        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpParseService, HttpParseService>(c =>
        {
        });
        
        services.AddHttpClient<ISpellCheckerService, SpellCheckerService>(c =>
        {
            c.BaseAddress = new Uri("http://speller.yandex.net/");
        });

        return services;
    }
}