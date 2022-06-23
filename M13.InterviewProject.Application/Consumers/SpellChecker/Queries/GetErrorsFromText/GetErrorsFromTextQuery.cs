using M13.InterviewProject.Application.Common;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromText;

public record GetErrorsFromTextQuery
{
    public string Text { get; init; }
}

public record GetErrorsFromTextQueryResponse : QueryResponse<List<SpellCheckError>>
{
    
}