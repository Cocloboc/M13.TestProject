using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromPage;

public record GetErrorsFromPageQuery
{
    public string Url { get; init; }
    public string RuleName { get; init; }
}

public record GetErrorsFromPageQueryResponse : QueryResponse<List<SpellCheckError>>
{
    
}