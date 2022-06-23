using M13.InterviewProject.Application.Common;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.CreateRule;

public record CreateRuleCommand
{
    public string Name { get; init; }
    public string Value { get; init; }
}