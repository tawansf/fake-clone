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
/// Implementação do modelo de IA utilizando via DeepSeek.
/// Responsável por enviar prompts e retornar respostas em formato JSON.
/// </summary>
/// <param name="httpClient">Instância de <see cref="HttpClient"/> usada para realizar as requisições HTTP.</param>
/// <param name="apiKey">Informe a chave de API da DeepSeek usada para autenticação.</param>
internal class DeepSeekAiProvider(HttpClient httpClient, string apiKey) : IAiProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    /// <summary>
    /// Gera uma resposta em formato JSON a partir de um prompt fornecido, utilizando o modelo da DeepSeek.
    /// </summary>
    /// <param name="prompt">Comando enviado pelo usuário.</param>
    /// <returns>Resposta gerada pela IA em formato de string e após isso você poderá usar o Deserialize</returns>
    public async Task<string> GenerateJsonAsync(string prompt)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.deepseek.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "deepseek-chat",
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