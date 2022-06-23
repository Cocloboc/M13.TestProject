using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.Consumers.Rule.Queries.TestRule;

public record GetTextFromPageQuery
{
    public string Url { get; init; }
    public string RuleName { get; init; }
}

public record GetTextFromPageQueryResponse : QueryResponse<PageFetchResult>
{
    
}
