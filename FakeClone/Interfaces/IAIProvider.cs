using System;
using System.Threading.Tasks;

namespace FakeClone.Interfaces;

/// <summary>
/// Interface para modelos de IA
/// </summary>
public interface IAiProvider
{
    /// <summary>
    /// Gera uma resposta da IA em formato JSON com base em um prompt fornecido.
    /// </summary>
    /// <param name="prompt">Texto com o comando ou pergunta enviado pelo usuário para a IA.</param>
    /// <returns>Uma <see cref="Task{TResult}"/> contendo uma string JSON com a resposta gerada pela IA.</returns>
    Task<string> GenerateJsonAsync(string prompt);
}