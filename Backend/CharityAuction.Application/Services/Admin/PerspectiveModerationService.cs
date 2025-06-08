using CharityAuction.Application.Interfaces;
using CharityAuction.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

public class PerspectiveModerationService : IContentModerationService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public PerspectiveModerationService(HttpClient httpClient, IOptions<PerspectiveApiOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
    }

    public async Task<(bool isFlagged, string? reason)> IsContentFlaggedAsync(string text)
    {
        var requestBody = new
        {
            comment = new { text },
            requestedAttributes = new Dictionary<string, object>
            {
                { "TOXICITY", new { } },
                { "SEVERE_TOXICITY", new { } },
                { "INSULT", new { } },
                { "PROFANITY", new { } },
                { "THREAT", new { } },
                { "IDENTITY_ATTACK", new { } }
            },

            languages = new[] { "en", "ru" }
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var requestUri = $"https://commentanalyzer.googleapis.com/v1alpha1/comments:analyze?key={_apiKey}";

        var response = await _httpClient.PostAsync(requestUri, content);
        response.EnsureSuccessStatusCode();

        var resultString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(resultString);

        var scores = json["attributeScores"] as JObject;
        if (scores == null)
            return (false, null);

        List<string> reasons = new();

        foreach (var attr in scores.Properties())
        {
            var name = attr.Name;
            var score = attr.Value["summaryScore"]?["value"]?.Value<float>() ?? 0;

            if (score >= 0.6f)
            {
                reasons.Add($"{name.Replace("_", " ")}: {score:F2}");
            }
        }

        if (reasons.Any())
        {
            return (true, string.Join(", ", reasons));
        }

        return (false, null);
    }


}
