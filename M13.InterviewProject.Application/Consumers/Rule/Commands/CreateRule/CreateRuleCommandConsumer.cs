using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.CreateRule;

public class CreateRuleCommandConsumer : IConsumer<CreateRuleCommand>
{
    private readonly IRuleService _ruleService;
    private readonly ILogger<CreateRuleCommandConsumer> _logger;

    public CreateRuleCommandConsumer(
        IRuleService ruleService,
        ILogger<CreateRuleCommandConsumer> logger)
    {
        _ruleService = ruleService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateRuleCommand> context)
    {
        var command = context.Message;
        try
        {
            var rule = new Models.Rule()
            {
                Name = command.Name,
                Value = command.Value
            };
            
            await _ruleService.CreateRuleAsync(rule, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(new CommandResponse());
    }
}