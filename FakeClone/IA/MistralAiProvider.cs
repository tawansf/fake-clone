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
/// Implementação do modelo de IA utilizando via OpenRouter.
/// Responsável por enviar prompts e retornar respostas em formato JSON.
/// </summary>
/// <param name="httpClient">Instância de HttpClient usada para realizar requisições HTTP.</param>
/// <param name="apiKey">Chave de API usada para autenticação com o serviço OpenRouter.</param>
public class MistralAiProvider(HttpClient httpClient, string apiKey): IAiProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Envia um prompt para o modelo Mistral e retorna a resposta gerada em formato JSON.
    /// </summary>
    /// <param name="prompt">Comando enviado pelo usuário para gerar a resposta.</param>
    /// <returns>Resposta da IA como uma string JSON. Retorna "[]" caso não haja resposta válida.</returns>
    public async Task<string> GenerateJsonAsync(string prompt)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "mistralai/Mistral-7B-Instruct-v0.2",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            Console.WriteLine("Enviando requisição para OpenRouter");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AiResponse>(resultString, JsonOptions);
            
            Console.WriteLine("Resposta obtida com sucesso.");

            return result?.Choices.FirstOrDefault()?.Message.Content ?? "[]";
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao carregar processar comando => {e.Message}");
            throw new Exception("Erro ao carregar processar comando.");
        }
    }
}