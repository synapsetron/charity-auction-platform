using CharityAuction.Application.Interfaces;
using CharityAuction.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
// ...

public class OpenAiModerationService : IContentModerationService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiModerationService(HttpClient httpClient, IOptions<OpenAIOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
    }

    public async Task<(bool isFlagged, string? reason)> IsContentFlaggedAsync(string text)
    {
        var request = new { input = text };
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/moderations", content);

        response.EnsureSuccessStatusCode();
        var resultString = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(resultString);

        bool flagged = result?.results[0]?.flagged ?? false;

        if (flagged)
        {
            var categories = result?.results[0]?.categories;
            List<string> triggered = new();

            foreach (var category in categories)
            {
                if ((bool)category.Value)
                    triggered.Add(category.Name);
            }

            return (true, string.Join(", ", triggered));
        }

        return (false, null);
    }
}
