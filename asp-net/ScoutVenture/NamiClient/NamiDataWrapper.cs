using System.Text.Json.Serialization;

namespace NamiClient
{
    public class NamiDataWrapper
    {
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("data")]
        public List<NamiMember>? Data { get; init; }
        
        [JsonPropertyName("responseType")]
        public string? ResponseType { get; init; }
        
        [JsonPropertyName("totalEntries")]
        public int TotalEntries { get; init; }
        
        [JsonPropertyName("message")]
        public string? Message { get; init; }
    }
}