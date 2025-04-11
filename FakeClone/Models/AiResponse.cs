using System.Collections.Generic;

namespace FakeClone.Models;

/// <summary>
/// Representa a resposta principal da IA, contendo uma lista de escolhas possíveis.
/// </summary>
internal class AiResponse
{
    /// <summary>
    /// Lista de escolhas retornadas pela IA como resposta ao prompt.
    /// </summary>
    public List<Choice> Choices { get; set; } = [];
}

/// <summary>
/// Representa uma das possíveis escolhas retornadas pela IA.
/// </summary>
internal class Choice
{
    /// <summary>
    /// Mensagem gerada pela IA para essa escolha.
    /// </summary>
    public Message Message { get; set; } = new();

    /// <summary>
    /// Indica o motivo pelo qual a geração da resposta foi finalizada (se aplicável).
    /// </summary>
    public string? FinishReason { get; set; }

    /// <summary>
    /// Índice da escolha na lista de respostas.
    /// </summary>
    public int Index { get; set; }
}

/// <summary>
/// Representa uma mensagem trocada na conversa com a IA.
/// </summary>
internal class Message
{
    /// <summary>
    /// Papel da mensagem (ex: "user", "assistant", "system").
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo textual da mensagem.
    /// </summary>
    public string Content { get; set; } = string.Empty;
}