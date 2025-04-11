# FakeClone

FakeClone é uma biblioteca .NET que usa IA para gerar dados fake de forma automática a partir de um modelo que o usuário poderá escolher e uma descrição com a solicitação dos campos a gerar. Ideal para popular banco de dados com seeds para testes em desenvolvimento.

## 🚀 Instalação
- Via [Nuget](https://www.nuget.org/packages/FakeClone/1.0.0)
```csharp
dotnet add package FakeClone
```

## ⚙️ Como Usar
#### **Registre o provider do modelo de IA no seu Startup ou Program.cs:**
```csharp

services.AddScoped<IAiProvider>(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    var client = factory.CreateClient();

    // Substitua "apiKey" pela sua chave real
    return new MistralAiProvider(client, apiKey!);
});
```
#### **Solicite a geração de dados com base em um prompt personalizado:**
```csharp

using var scope = serviceProvider.CreateScope();

var seedGenerator = scope.ServiceProvider.GetRequiredService<ISeedGenerator>();

var request = new SeedRequest
{
    Prompt = "Gere 10 usuários fictícios com nome, e-mail, senha, gênero e data de aniversário"
};
```
#### **(Opcional) Salve os dados no banco de dados:**

```csharp
// Em breve será implementado o suporte nativo à persistência via DbContext.

// Exemplo:
context.Users.AddRange(users);
await context.SaveChangesAsync();
```
## 🌐 Provedores de IA Suportados

- ✅ [Mistral AI](https://mistral.ai/) - Disponível a partir da versão 1.0.1 
  - Gere sua chave de api clicando [aqui](https://openrouter.ai/)
- Llama - (Em breve, fase de testes)
- Grok - (Em breve)
- OpenAI - (Em breve)

## 📦 Requisitos e Funcionalidades

- Atualmente a biblioteca suporta apenas .NET 9 (mas em breve irei expandir para outras versões

- Integração com modelos de IA via OpenRouter. Em breve: (OpenAI, llama, Grok)

- Geração de listas de objetos diretamente em JSON

- Adaptado para múltiplos modelos e providers

- Facilidade para testes, desenvolvimento e mocking de dados

## 🔐 Dicas de segurança

- Proteja suas API Keys 😊
- Use o `dotnet secrets`

## 📄 Licença

- MIT