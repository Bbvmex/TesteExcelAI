using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VbeAddin.AI
{
    public class LlmApiClient
    {
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(120)
        };

        private readonly string _baseUrl;
        private readonly string _model;

        private const string SystemPrompt =
            "You are an expert VBA and RPA programmer assistant embedded in the Excel VBA Editor. " +
            "Help the user write, debug, and understand VBA code. " +
            "Keep answers concise and include code snippets when helpful.";

        public LlmApiClient(string baseUrl, string model)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _model = model;
        }

        public async Task<string> AskAsync(string userMessage, CancellationToken ct = default)
        {
            var payload = new ChatRequest
            {
                Model = _model,
                MaxTokens = 2048,
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "system", Content = SystemPrompt },
                    new ChatMessage { Role = "user",   Content = userMessage }
                }
            };

            string json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string endpoint = $"{_baseUrl}/chat/completions";
            HttpResponseMessage response = await _http.PostAsync(endpoint, content, ct);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<ChatResponse>(body);

            if (parsed?.Choices == null || parsed.Choices.Count == 0)
                throw new InvalidOperationException("Empty response from local LLM.");

            return parsed.Choices[0].Message.Content;
        }
    }
}
