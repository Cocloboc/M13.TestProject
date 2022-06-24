using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsCountFromPage;

public record GetErrorsCountFromPageQuery
{
    public string Url { get; init; }
    public string RuleName { get; init; }
}

public record GetErrorsCountFromPageQueryResponse : QueryResponse<SpellCheckErrorCounter>
{
    
}