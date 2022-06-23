using M13.InterviewProject.Application.Consumers.Rule.Queries.TestRule;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromText;
using M13.InterviewProject.Application.Models;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SpellCheckerController
{
    private readonly IMediator _mediator;

    public SpellCheckerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Page/Text")]
    public async Task<PageFetchResult> GetTextFromPage(GetTextFromPageQuery query, CancellationToken token)
    {
        var response = await _mediator
            .CreateRequestClient<GetTextFromPageQuery>()
            .GetResponse<GetTextFromPageQueryResponse>(query, token);
        
        return response.Message.Data;
    }
    
    [HttpPost("Page/Errors")]
    public async Task<PageFetchResult> GetErrorsFromPage(GetTextFromPageQuery query, CancellationToken token)
    {
        var response = await _mediator
            .CreateRequestClient<GetTextFromPageQuery>()
            .GetResponse<GetTextFromPageQueryResponse>(query, token);
        
        return response.Message.Data;
    }
    
    [HttpPost("Text/Errors")]
    public async Task<List<SpellCheckError>> GetErrorsFromText(GetErrorsFromTextQuery query, CancellationToken token)
    {
        var response = await _mediator
            .CreateRequestClient<GetErrorsFromTextQuery>()
            .GetResponse<GetErrorsFromTextQueryResponse>(query, token);
        
        return response.Message.Data;
    }
}