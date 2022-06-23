using M13.InterviewProject.Application.Common;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.DeleteRule;

public record DeleteRuleCommand
{
    public string Name { get; init; }
}