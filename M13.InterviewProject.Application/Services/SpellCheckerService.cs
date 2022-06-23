using M13.InterviewProject.Application.Models;
using Newtonsoft.Json;

namespace M13.InterviewProject.Application.Services;

public class SpellCheckerService : ISpellCheckerService
{
    private readonly string domain = "http://speller.yandex.net/services/spellservice.json/checkText";
    private readonly HttpClient _client;

    public SpellCheckerService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<SpellerErrors>> CheckTextForErrorsAsync(string text, CancellationToken token = default)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("text", text)
        });
        var result = await _client.PostAsync("services/spellservice.json/checkText", content, token);
        var resultContentJson = await result.Content.ReadAsStringAsync(token);
        
        return JsonConvert.DeserializeObject<List<SpellerErrors>>(resultContentJson);
    }
}