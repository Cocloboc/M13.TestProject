using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.Services;

public interface IRuleService
{
    Task CreateRuleAsync(Rule rule, CancellationToken token = default);
    Task UpdateRuleAsync(Rule rule, CancellationToken token = default);
    Task DeleteRuleAsync(string ruleName, CancellationToken token = default);
    Task<Rule> GetRuleAsync(string ruleName, CancellationToken token = default);
}