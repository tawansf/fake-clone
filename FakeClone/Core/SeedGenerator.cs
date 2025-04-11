using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FakeClone.Interfaces;

namespace FakeClone.Core;

public sealed class SeedGenerator(IAiProvider iaiProvider) : ISeedGenerator
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<T>> GenerateAsync<T>(SeedRequest request) where T : class
    {
        var prompt = PromptBuilder.Build<T>(request.Prompt);

        var json = await iaiProvider.GenerateJsonAsync(prompt);

        var result = JsonSerializer.Deserialize<List<T>>(json, JsonOptions);

        return result ?? [];
    }
}