using M13.InterviewProject.Application.Models;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.Rule.Queries.TestRule;

public class GetTextFromPageQueryConsumer: IConsumer<GetTextFromPageQuery>
{
    private readonly IRuleService _ruleService;
    private readonly ILogger<GetTextFromPageQueryConsumer> _logger;
    private readonly IHttpParseService _parseService;

    public GetTextFromPageQueryConsumer(
        IRuleService ruleService,
        ILogger<GetTextFromPageQueryConsumer> logger,
        IHttpParseService parseService)
    {
        _ruleService = ruleService;
        _logger = logger;
        _parseService = parseService;
    }

    public async Task Consume(ConsumeContext<GetTextFromPageQuery> context)
    {
        var query = context.Message;
        GetTextFromPageQueryResponse response;
        try
        {
            var ruleName = !string.IsNullOrEmpty(query.RuleName) ? query.RuleName : new Uri(query.Url).Host;
            var rule = await _ruleService.GetRuleAsync(ruleName, context.CancellationToken);

            var textFromPage = await _parseService.FetchTextFromPageAsync(query.Url, rule.Value, context.CancellationToken);

            var fetchResult = new PageFetchResult {Url = query.Url, Content = textFromPage};
            response = new GetTextFromPageQueryResponse {Data = fetchResult};
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(response);
    }
}