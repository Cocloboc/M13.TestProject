using System.Text;
using HtmlAgilityPack;

namespace M13.InterviewProject.Application.Services;

public class HttpParseService : IHttpParseService
{
    private readonly HttpClient _httpClient;

    public HttpParseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<string> DownloadPageAsync(string url, CancellationToken token = default)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(url, token);
        using HttpContent content = response.Content;
        var json = await content.ReadAsStringAsync(token);

        return json;
    }
    
    public async Task<string> FetchTextFromPageAsync(string url, string xpath, CancellationToken token = default)
    {
        var page = await DownloadPageAsync(url, token);
            
        var document = new HtmlDocument();
        document.LoadHtml(page);
            
        var innerTextBuilder = new StringBuilder();
        var nodes = document.DocumentNode.SelectNodes(xpath);

        if (nodes == null)
        {
            return string.Empty;
        }
            
        foreach (var node in nodes)
        {
            innerTextBuilder.AppendLine(node.InnerText);
        }

        return innerTextBuilder.ToString();
    }
}