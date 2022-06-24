using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsCountFromPage;

public class GetErrorsCountFromPageQueryConsumer: IConsumer<GetErrorsCountFromPageQuery>
{
    private readonly IRuleService _ruleService;
    private readonly IHttpParseService _httpParseService;
    private readonly ISpellCheckerService _spellCheckerService;
    private readonly ILogger<GetErrorsCountFromPageQueryConsumer> _logger;

    public GetErrorsCountFromPageQueryConsumer(
        IRuleService ruleService,
        IHttpParseService httpParseService,
        ISpellCheckerService spellCheckerService,
        ILogger<GetErrorsCountFromPageQueryConsumer> logger)
    {
        _ruleService = ruleService;
        _httpParseService = httpParseService;
        _spellCheckerService = spellCheckerService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetErrorsCountFromPageQuery> context)
    {
        var query = context.Message;
        GetErrorsCountFromPageQueryResponse response;
        try
        {
            var ruleName = !string.IsNullOrEmpty(query.RuleName) ? query.RuleName : new Uri(query.Url).Host;
            var rule = await _ruleService.GetRuleAsync(ruleName, context.CancellationToken);
            
            var pageText = await _httpParseService.FetchTextFromPageAsync(query.Url, rule.Value, context.CancellationToken);
            var errors = await _spellCheckerService.CheckTextForErrorsAsync(pageText);

            var counter = new SpellCheckErrorCounter() {ErrorsCount = errors.Count};
            
            response = new GetErrorsCountFromPageQueryResponse {Data = counter};
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(response);
    }
}