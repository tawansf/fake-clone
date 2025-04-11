using System;
using System.Reflection;
using System.Text;

namespace FakeClone;

public static class PromptBuilder
{
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
        sb.AppendLine("Retorne apenas uma lista JSON do modelo.");

        return sb.ToString();
    }
    
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