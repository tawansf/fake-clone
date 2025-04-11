using System;
using System.Threading.Tasks;
using FakeClone.Interfaces;

namespace FakeClone.IA;

public class OpenAiProvider(string apiKey): IAiProvider 
{
    // TODO: Melhoria -> Implementar o modelo da OpenAI
    public Task<string> GenerateJsonAsync(string prompt)
    {
        throw new NotImplementedException();
    }
}