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

/// <summary>
/// Implementação do modelo de IA via API OpenRouter.
/// Responsável por enviar prompts e receber respostas em formato JSON.
/// </summary>
/// <param name="httpClient">Instância de HttpClient utilizada para realizar requisições HTTP.</param>
/// <param name="apiKey">Chave de API utilizada para autenticação com o serviço do OpenRouter.</param>
public class LlamaMaverickAiProvider(HttpClient httpClient, string apiKey) : IAiProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    /// <summary>
    /// Envia um prompt para o modelo LLaMA via OpenRouter e retorna a resposta gerada em formato JSON.
    /// </summary>
    /// <param name="prompt">Comando que será enviado para a IA.</param>
    /// <returns>Resposta da IA como uma string JSON.</returns>
    public async Task<string> GenerateJsonAsync(string prompt)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            request.Headers.Add("HTTP-Referer", "https://github.com/tawansf/fake-clone");
            request.Headers.Add("X-Title", "FakeClone Seed Generator");

            var requestBody = new
            {
                model = "meta-llama/llama-4-maverick:free",
                messages = new[]
                {
                    new
                    {
                        role = "user", 
                        content = prompt
                    }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var resultString = await response.Content.ReadAsStringAsync();

            var aiResponse = JsonSerializer.Deserialize<AiResponse>(resultString, JsonOptions);

            return aiResponse?.Choices.FirstOrDefault()?.Message?.Content ?? "[]";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}