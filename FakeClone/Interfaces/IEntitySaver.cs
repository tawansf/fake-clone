using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FakeClone.Interfaces;

public interface IEntitySaver
{
    Task SaveAsync<T>(IEnumerable<T> entities, DbContext context) where T : class;
}