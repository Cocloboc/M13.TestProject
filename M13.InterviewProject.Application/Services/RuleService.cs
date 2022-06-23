using M13.InterviewProject.Application.Exceptions;
using M13.InterviewProject.Application.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace M13.InterviewProject.Application.Services;

public class RuleService : IRuleService
{
    public readonly string CachePrefix = "Rules";
    
    private readonly IDistributedCache _cache;

    public RuleService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task CreateRuleAsync(Rule rule, CancellationToken token = default)
    {
        var ruleValue = await _cache.GetStringAsync($"{CachePrefix}:{rule.Name}", token: token);
        if (ruleValue != null)
        {
            throw new HasDuplicateExceptions($"Rule with the name `{rule.Name}` already exists");
        }

        await _cache.SetStringAsync($"{CachePrefix}:{rule.Name}", rule.Value, token);
    }

    public async Task UpdateRuleAsync(Rule rule, CancellationToken token = default)
    {
        var ruleValue = await _cache.GetStringAsync($"{CachePrefix}:{rule.Name}", token: token);
        if (ruleValue == null)
        {
            throw new NotFoundException($"Rule with the name `{rule.Name}` is not found");
        }

        await _cache.SetStringAsync($"{CachePrefix}:{rule.Name}", rule.Value, token);
    }

    public async Task DeleteRuleAsync(string ruleName, CancellationToken token = default)
    {
        await _cache.RemoveAsync($"{CachePrefix}:{ruleName}", token);
    }

    public async Task<Rule> GetRuleAsync(string ruleName, CancellationToken token = default)
    {
        var ruleValue = await _cache.GetStringAsync($"{CachePrefix}:{ruleName}", token: token);
        if (ruleValue == null)
        {
            throw new NotFoundException($"Rule with the name `{ruleName}` is not found");
        }

        return new Rule() {Name = ruleName, Value = ruleValue};
    }
}