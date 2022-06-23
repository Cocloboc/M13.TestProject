using M13.InterviewProject.Application.Common;

namespace M13.InterviewProject.Application.Consumers.Rule.Queries.GetRule;

public record GetRuleQuery
{
    public string Name { get; init; }
}

public record GetRuleQueryResponse : QueryResponse<Models.Rule>
{
}