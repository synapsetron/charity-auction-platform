using CharityAuction.Application.Interfaces;
using CharityAuction.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
// ...

[Obsolete("OpenAiModerationService is deprecated and replaced by PerspectiveModerationService.")]
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
        const int maxRetries = 3;
        int retryCount = 0;
        TimeSpan delay = TimeSpan.FromSeconds(2);

        var requestBody = new
        {
            input = text,
            model = "omni-moderation-latest"
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        while (retryCount <= maxRetries)
        {
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/moderations", content);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                retryCount++;
                Console.WriteLine($"[Moderation] Rate limited. Retrying {retryCount}/{maxRetries}...");
                await Task.Delay(delay);
                delay += TimeSpan.FromSeconds(2); // backoff
                continue;
            }

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

        throw new Exception("OpenAI moderation rate limit exceeded after multiple retries.");
    }
}
