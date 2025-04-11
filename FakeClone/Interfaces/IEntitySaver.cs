using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FakeClone.Interfaces;

/// <summary>
/// Interface para salvar as entidades no banco de dados.
/// </summary>
internal interface IEntitySaver
{
    /// <summary>
    /// Salva uma lista de entidades geradas pela IA no banco de dados utilizando o <see cref="DbContext"/> fornecido.
    /// </summary>
    /// <typeparam name="T">O tipo da entidade a ser salva. Deve ser uma classe.</typeparam>
    /// <param name="entities">Coleção de entidades do tipo <typeparamref name="T"/> a serem persistidas.</param>
    /// <param name="context">Instância do <see cref="DbContext"/> utilizada para salvar os dados.</param>
    /// <returns>Uma <see cref="Task"/> representando a operação assíncrona.</returns>
    Task SaveAsync<T>(IEnumerable<T> entities, DbContext context) where T : class;
}