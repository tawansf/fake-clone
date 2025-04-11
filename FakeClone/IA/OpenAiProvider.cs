using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FakeClone.Interfaces;
using FakeClone.Models;

namespace FakeClone.IA;

public class OpenAiProvider(HttpClient httpClient, string apiKey): IAiProvider 
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<string> GenerateJsonAsync(string prompt)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<AiResponse>(resultString, JsonOptions);
            return result?.Choices.FirstOrDefault()?.Message.Content ?? "[]";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}