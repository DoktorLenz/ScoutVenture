using System.Text.Json.Serialization;

namespace NamiClient
{
    public class NamiErrorWrapper
    {
        [JsonPropertyName("statusCode")] public int StatusCode { get; set; }

        [JsonPropertyName("statusMessage")] public string StatusMessage { get; set; }
    }
}