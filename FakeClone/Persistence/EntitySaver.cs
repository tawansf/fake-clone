using System.Collections.Generic;
using System.Threading.Tasks;
using FakeClone.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeClone.Persistence;

public class EntitySaver: IEntitySaver
{
    public async Task SaveAsync<T>(IEnumerable<T> entities, DbContext context) where T : class
    {
        context.Set<T>().AddRange(entities);
        await context.SaveChangesAsync();
    }
}