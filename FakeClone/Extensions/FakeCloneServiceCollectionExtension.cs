using FakeClone.Core;
using FakeClone.IA;
using FakeClone.Interfaces;
using FakeClone.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FakeClone.Extensions;

public static class FakeCloneServiceCollectionExtension
{
    public static IServiceCollection AddFakeClone(this IServiceCollection services)
    {
        services.AddScoped<IEntitySaver, EntitySaver>();
        services.AddScoped<ISeedGenerator, SeedGenerator>();
        return services;
    }
}