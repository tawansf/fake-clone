using System.Collections.Generic;
using System.Threading.Tasks;
using FakeClone.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeClone.Persistence;

/// <summary>
/// Interface que persiste as entidades no banco de dados usando um DbContext.
/// </summary>
public class EntitySaver : IEntitySaver
{
    /// <summary>
    /// Salva uma coleção de entidades no banco de dados usando o DbContext fornecido.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade a ser salva.</typeparam>
    /// <param name="entities">Coleção de entidades a serem persistidas.</param>
    /// <param name="context">Instância do DbContext utilizada para persistência.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona de salvamento.</returns>
    public async Task SaveAsync<T>(IEnumerable<T> entities, DbContext context) where T : class
    {
        context.Set<T>().AddRange(entities);
        await context.SaveChangesAsync();
    }
}