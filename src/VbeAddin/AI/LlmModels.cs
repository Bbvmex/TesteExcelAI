using System.Collections.Generic;
using Newtonsoft.Json;

namespace VbeAddin.AI
{
    public class ChatRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("messages")]
        public List<ChatMessage> Messages { get; set; }

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 2048;

        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;
    }

    public class ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class ChatResponse
    {
        [JsonProperty("choices")]
        public List<ChatChoice> Choices { get; set; }
    }

    public class ChatChoice
    {
        [JsonProperty("message")]
        public ChatMessage Message { get; set; }
    }
}
