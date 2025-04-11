using System.Collections.Generic;
using System.Threading.Tasks;
using FakeClone.Core;

namespace FakeClone.Interfaces;

/// <summary>
/// Interface para modelos de IA
/// </summary>
public interface ISeedGenerator
{
    /// <summary>
    /// Gera uma lista de objetos do tipo <typeparamref name="T"/> com base nos comandos informados pelo usuário.
    /// </summary>
    /// <typeparam name="T">Um tipo da entidade a ser gerada. Deve ser uma classe.</typeparam>
    /// <param name="request">Um objeto <see cref="SeedRequest"/> contendo as instruções para a geração dos dados.</param>
    /// <returns>Uma lista de objetos do tipo <typeparamref name="T"/>.</returns>
    Task<List<T>> GenerateAsync<T>(SeedRequest request) where T : class;
}