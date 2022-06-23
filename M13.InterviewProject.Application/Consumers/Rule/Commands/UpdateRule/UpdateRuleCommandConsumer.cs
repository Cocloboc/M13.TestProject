using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.UpdateRule;

public class UpdateRuleCommandConsumer: IConsumer<UpdateRuleCommand>
{
    private readonly IRuleService _ruleService;
    private readonly ILogger<UpdateRuleCommandConsumer> _logger;

    public UpdateRuleCommandConsumer(
        IRuleService ruleService,
        ILogger<UpdateRuleCommandConsumer> logger)
    {
        _ruleService = ruleService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UpdateRuleCommand> context)
    {
        var command = context.Message;
        try
        {
            var rule = new Models.Rule()
            {
                Name = command.Name,
                Value = command.Value
            };
            
            await _ruleService.UpdateRuleAsync(rule, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(new CommandResponse());
    }
}