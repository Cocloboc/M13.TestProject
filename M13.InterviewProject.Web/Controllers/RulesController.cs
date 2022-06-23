using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Consumers.Rule.Commands.CreateRule;
using M13.InterviewProject.Application.Consumers.Rule.Commands.DeleteRule;
using M13.InterviewProject.Application.Consumers.Rule.Commands.UpdateRule;
using M13.InterviewProject.Application.Consumers.Rule.Queries.GetRule;
using M13.InterviewProject.Application.Consumers.Rule.Queries.TestRule;
using M13.InterviewProject.Application.Models;
using M13.InterviewProject.Web.Models;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class RulesController : Controller
{
    private readonly IMediator _mediator;

    public RulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddRule(CreateRuleCommand command, CancellationToken token)
    {
        await _mediator
            .CreateRequestClient<CreateRuleCommand>()
            .GetResponse<CommandResponse>(command, token);
        
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateRule(UpdateRuleCommand command, CancellationToken token)
    {
        await _mediator
            .CreateRequestClient<UpdateRuleCommand>()
            .GetResponse<CommandResponse>(command, token);
        
        return Ok();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteRule(string name, CancellationToken token)
    {
        var command = new DeleteRuleCommand {Name = name};

        var validator = new DeleteRuleCommandValidator();
        var result = await validator.ValidateAsync(command, token);

        if (!result.IsValid)
        {
            return new BadRequestObjectResult(result.Errors);
        }
        await _mediator
            .CreateRequestClient<DeleteRuleCommand>()
            .GetResponse<CommandResponse>(command, token);
        
        return Ok();
    }
    
    [HttpGet("{name}")]
    public async Task<Rule> GetRule(string name, CancellationToken token)
    {
        var query = new GetRuleQuery() {Name = name};
        var response = await _mediator
            .CreateRequestClient<GetRuleQuery>()
            .GetResponse<GetRuleQueryResponse>(query, token);
        
        return response.Message.Data;
    }
}