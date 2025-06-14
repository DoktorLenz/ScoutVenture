using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NamiClient
{
    public class NamiMember
    {
        [JsonPropertyName("entries_mitgliedsNummer")]
        public long MemberId { get; init; }
        
        [JsonPropertyName("entries_vorname")]
        public string? FirstName { get; init; }
        
        [JsonPropertyName("entries_nachname")]
        public string? LastName { get; init; }
        
        [JsonPropertyName("entries_geburtsDatum")]
        [JsonConverter(typeof(NamiDateOnlyConverter))]
        public DateOnly? BirthDate { get; init; }
        
        [JsonPropertyName("entries_stufe")]
        public string? Rank { get; init; }
        
        [JsonPropertyName("entries_geschlecht")]
        public string? Gender { get; init; }
    }
}