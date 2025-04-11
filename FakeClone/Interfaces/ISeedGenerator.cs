using System.Collections.Generic;
using System.Threading.Tasks;
using FakeClone.Core;

namespace FakeClone.Interfaces;

public interface ISeedGenerator
{
    Task<List<T>> GenerateAsync<T>(SeedRequest request) where T : class;
}