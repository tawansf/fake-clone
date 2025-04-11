using System;
using System.Threading.Tasks;

namespace FakeClone.Interfaces;

public interface IAiProvider
{
    Task<string> GenerateJsonAsync(string prompt);
}