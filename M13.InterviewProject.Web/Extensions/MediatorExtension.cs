using M13.InterviewProject.Application.Consumers;
using MassTransit;

namespace M13.InterviewProject.Web.Extensions;

public static class MediatorExtension
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediator(cfg =>
        {
            cfg.AddConsumersFromNamespaceContaining<RootConsumer>();
        });

        return services;
    }
}