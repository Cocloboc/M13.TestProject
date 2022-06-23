namespace M13.InterviewProject.Application.Services;

public interface IHttpParseService
{
    Task<string> FetchTextFromPageAsync(string url, string xpath, CancellationToken token = default);
}