using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.Services;

public interface ISpellCheckerService
{
    Task<List<SpellerErrors>> CheckTextForErrorsAsync(string text, CancellationToken token = default);
}