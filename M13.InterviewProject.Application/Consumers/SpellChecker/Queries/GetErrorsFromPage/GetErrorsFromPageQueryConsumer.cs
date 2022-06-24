using AutoMapper;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromText;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromPage;

public class GetErrorsFromPageQueryConsumer: IConsumer<GetErrorsFromPageQuery>
{
    private readonly IRuleService _ruleService;
    private readonly IHttpParseService _httpParseService;
    private readonly ISpellCheckerService _spellCheckerService;
    private readonly IMapper _mapper;
    private readonly ILogger<GetErrorsFromPageQueryConsumer> _logger;

    public GetErrorsFromPageQueryConsumer(
        IRuleService ruleService,
        IHttpParseService httpParseService,
        ISpellCheckerService spellCheckerService,
        IMapper mapper,
        ILogger<GetErrorsFromPageQueryConsumer> logger)
    {
        _ruleService = ruleService;
        _httpParseService = httpParseService;
        _spellCheckerService = spellCheckerService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetErrorsFromPageQuery> context)
    {
        var query = context.Message;
        GetErrorsFromPageQueryResponse response;
        try
        {
            var ruleName = !string.IsNullOrEmpty(query.RuleName) ? query.RuleName : new Uri(query.Url).Host;
            var rule = await _ruleService.GetRuleAsync(ruleName, context.CancellationToken);
            
            var pageText = await _httpParseService.FetchTextFromPageAsync(query.Url, rule.Value, context.CancellationToken);
            var errors = await _spellCheckerService.CheckTextForErrorsAsync(pageText);
            
            var errorsDto = _mapper.Map<List<SpellCheckError>>(errors);
            
            response = new GetErrorsFromPageQueryResponse {Data = errorsDto};
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(response);
    }
}