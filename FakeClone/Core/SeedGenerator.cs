using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FakeClone.Interfaces;

namespace FakeClone.Core;

/// <summary>
/// Responsável por gerar dados fictícios com base em um prompt, utilizando um provedor de IA.
/// </summary>
/// <param name="iaiProvider">Provedor de IA responsável pela geração do conteúdo.</param>
public sealed class SeedGenerator(IAiProvider iaiProvider) : ISeedGenerator
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Gera uma lista de objetos do tipo especificado, com base no prompt da requisição.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto a ser gerado.</typeparam>
    /// <param name="request">Requisição contendo o prompt para geração.</param>
    /// <returns>Lista de objetos fictícios do tipo <typeparamref name="T"/>.</returns>
    public async Task<List<T>> GenerateAsync<T>(SeedRequest request) where T : class
    {
        var prompt = PromptBuilder.Build<T>(request.Prompt);

        var json = await iaiProvider.GenerateJsonAsync(prompt);

        var result = JsonSerializer.Deserialize<List<T>>(json, JsonOptions);

        return result ?? [];
    }
}