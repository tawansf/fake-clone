using FakeClone.Core;
using FakeClone.IA;
using FakeClone.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var apiKey = configuration["DeepSeek:ApiKey"];

        services.AddHttpClient();

        services.AddScoped<IAiProvider>(provider =>
        {
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient();
            return new DeepSeekAiProvider(client, apiKey!);
        });

        services.AddScoped<ISeedGenerator, SeedGenerator>();
    });

var host = builder.Build();

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