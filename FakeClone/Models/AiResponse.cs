using System.Collections.Generic;

namespace FakeClone.Models;

internal class AiResponse
{
    public List<Choice> Choices { get; set; } = [];
}

internal class Choice
{
    public Message Message { get; set; } = new();
}

internal class Message
{
    public string Content { get; set; } = string.Empty;
}