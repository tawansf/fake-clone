using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FakeClone.Interfaces;

namespace FakeClone.IA;

public class OpenRouterAiProvider(HttpClient httpClient) : IAiProvider
{
    public async Task<string> GenerateJsonAsync(string prompt)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
            
            // TODO: Melhoria -> Encontrar uma forma de passar a API_KEY
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "API_KEY_AQUI");
            request.Headers.Add("HTTP-Referer", "https://github.com/tawansf/fake-clone");
            request.Headers.Add("X-Title", "FakeClone Seed Generator");

            var requestBody = new
            {
                model = "openai/gpt-4o",
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

            using var doc = JsonDocument.Parse(resultString);
            var content = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return content ?? "[]";
        }
        catch (Exception e)
        {
            // TODO: Melhoria -> Criar um tratamento de erros melhor
            Console.WriteLine(e);
            throw;
        }
    }
}