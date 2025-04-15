using FakeClone.Core;
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
        var apiKey = configuration["OpenRouter:ApiKey"];

        services.AddHttpClient();

        services.AddScoped<IAiProvider>(provider =>
        {
            var factory = provider.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient();
            return new FakeClone.IA.MistralAiProvider(client, apiKey);
        });

        services.AddScoped<ISeedGenerator, SeedGenerator>();
    });

var host = builder.Build();

using var scope = host.Services.CreateScope();

var seedGenerator = scope.ServiceProvider.GetRequiredService<ISeedGenerator>();

var result = await seedGenerator.GenerateAsync<MelhoresFilmesImdb>(new SeedRequest
{
    Prompt = "Faça uma lista de 5 filmes da disney, e retorne apenas o nome dele no formato JSON"
});

foreach (var user in result)
{
    Console.WriteLine($"{user.NomeFilme} - {user.NomeFilme}");
}
public class User
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime Birthday { get; set; } = default!;
}

public class EstadosBrasileiros
{
    public string NomeCompleto { get; set; } = default!;
    public string Sigla { get; set; } = default!;
}

public class MelhoresFilmesImdb
{
    public string NomeFilme { get; set; } = default!;
}