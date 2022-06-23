using M13.InterviewProject.Application.Exceptions;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.Rule.Queries.GetRule;

public class GetRuleQueryConsumer: IConsumer<GetRuleQuery>
{
    private readonly IRuleService _ruleService;
    private readonly ILogger<GetRuleQueryConsumer> _logger;

    public GetRuleQueryConsumer(
        IRuleService ruleService,
        ILogger<GetRuleQueryConsumer> logger)
    {
        _ruleService = ruleService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetRuleQuery> context)
    {
        var query = context.Message;
        GetRuleQueryResponse response;
        try
        {
            var rule = await _ruleService.GetRuleAsync(query.Name, context.CancellationToken);
            
            response = new GetRuleQueryResponse() {Data = rule};
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(response);
    }
}