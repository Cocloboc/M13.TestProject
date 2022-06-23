using AutoMapper;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromText;

public class GetErrorsFromTextQueryConsumer: IConsumer<GetErrorsFromTextQuery>
{
    private readonly ISpellCheckerService _spellCheckerService;
    private readonly IMapper _mapper;
    private readonly ILogger<GetErrorsFromTextQueryConsumer> _logger;

    public GetErrorsFromTextQueryConsumer(
        ISpellCheckerService spellCheckerService,
        IMapper mapper,
        ILogger<GetErrorsFromTextQueryConsumer> logger)
    {
        _spellCheckerService = spellCheckerService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetErrorsFromTextQuery> context)
    {
        var query = context.Message;
        GetErrorsFromTextQueryResponse response;
        try
        {
            var errors = await _spellCheckerService.CheckTextForErrorsAsync(query.Text);
            var errorsDto = _mapper.Map<List<SpellCheckError>>(errors);
            
            response = new GetErrorsFromTextQueryResponse {Data = errorsDto};
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(response);
    }
}