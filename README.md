# FakeClone

FakeClone é uma biblioteca .NET que usa IA para gerar dados fake de forma automática a partir de um modelo que o usuário poderá escolher e uma descrição com a solicitação dos campos a gerar. Ideal para popular banco de dados com seeds para testes em desenvolvimento.

## 🚀 Instalação

```csharp
dotnet add package FakeClone
```

## ⚙️ Como Usar

```csharp
// Instanciamos o nosso generator e informamos a nossa API_KEY referente ao modelo escolhido
var generator = new SeedGenerator(new OpenRouterAiProvider("SUA_API_KEY"));

// Solicito as informações a serem geradas
var request = new SeedRequest("Gere 10 usuários com nome, e-mail e idade");

// generateAsync <T> é um método genérico, é aconselhável você informar e Entidade a ser salva no banco.
var users = await generator.GenerateAsync<User>(request);

// Em breve irei implementar uma forma de salvar as seeds no banco de dados usando o contexto.
// Exemplo: context.SaveChangesAsync(token);
```

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