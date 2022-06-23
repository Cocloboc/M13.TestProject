using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.DeleteRule;

public class DeleteRuleCommandConsumer: IConsumer<DeleteRuleCommand>
{
    private readonly IRuleService _ruleService;
    private readonly ILogger<DeleteRuleCommandConsumer> _logger;

    public DeleteRuleCommandConsumer(
        IRuleService ruleService,
        ILogger<DeleteRuleCommandConsumer> logger)
    {
        _ruleService = ruleService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DeleteRuleCommand> context)
    {
        var command = context.Message;
        try
        {
            await _ruleService.DeleteRuleAsync(command.Name, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError($"We have some errors:{e.Message}", e);
            throw;
        }

        await context.RespondAsync(new CommandResponse());
    }
}