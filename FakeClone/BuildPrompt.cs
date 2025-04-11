using System;
using System.Reflection;
using System.Text;

namespace FakeClone;

/// <summary>
/// Classe utilitária responsável por construir prompts para geração de dados fictícios a partir de um modelo genérico.
/// </summary>
public static class PromptBuilder
{
    /// <summary>
    /// Constrói um prompt de texto baseado em um tipo de modelo e uma mensagem fornecida pelo usuário.
    /// </summary>
    /// <typeparam name="T">Tipo do modelo para o qual os dados fictícios serão gerados.</typeparam>
    /// <param name="userMessage">Mensagem ou requisito adicional definido pelo usuário.</param>
    /// <returns>String contendo o prompt estruturado para a IA gerar os dados.</returns>
    public static string Build<T>(string userMessage) where T : class
    {
        var type = typeof(T);
        var sb = new StringBuilder();

        sb.AppendLine("Aja como um gerador de dados fictícios para o seguinte modelo:");
        sb.AppendLine($"Modelo: {type.Name}");
        sb.AppendLine("Campos:");

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            sb.AppendLine($"- {prop.Name}: {Map(prop.PropertyType)}");
        }

        sb.AppendLine();
        sb.AppendLine($"Requisito: {userMessage}");
        sb.AppendLine("Retorne apenas uma lista JSON do modelo e responda apenas o JSON sem explicações.");

        return sb.ToString();
    }
    
    /// <summary>
    /// Mapeia um tipo CLR para uma descrição textual simplificada usada na construção do prompt.
    /// </summary>
    /// <param name="type">Tipo a ser mapeado.</param>
    /// <returns>Descrição textual correspondente ao tipo.</returns>
    private static string Map(Type type)
    {
        // TODO: Melhoria -> Permitir mais tipos de dados
        if (type == typeof(string)) return "texto";
        if (type == typeof(int)) return "número inteiro";
        if (type == typeof(Guid)) return "UUID";
        if (type == typeof(DateTime)) return "data";
        if (type == typeof(bool)) return "verdadeiro ou falso";
        if (type == typeof(decimal) || type == typeof(double)) return "número decimal";
        return type.Name.ToLower();
    }
}