namespace FakeClone.Core;

/// <summary>
/// Requisição do prompt enviado pelo usuário.
/// </summary>
public sealed class SeedRequest
{
    /// <summary>
    /// Comando enviado pelo usuário para a geração de dados.
    /// </summary>
    public required string Prompt { get; set; }
}