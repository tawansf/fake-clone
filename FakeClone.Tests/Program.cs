using System.Net.Http.Headers;
using FakeClone.Core;
using FakeClone.IA;
using FakeClone.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddScoped<IAiProvider, OpenRouterAiProvider>();
        services.AddScoped<ISeedGenerator, SeedGenerator>();
        services.AddTransient<OpenRouterAiProvider>(provider =>
        {
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient();
            return new OpenRouterAiProvider(client);
        });
    })
    .Build();

using var scope = host.Services.CreateScope();

var seedGenerator = scope.ServiceProvider.GetRequiredService<ISeedGenerator>();

var result = await seedGenerator.GenerateAsync<User>(new SeedRequest
{
    Prompt = "Gere 1 usuário fictício com nome e e-mail"
});

foreach (var user in result)
{
    Console.WriteLine($"{user.Name} - {user.Email}");
}
public class User
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}