using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NamiClient
{
    public class NamiMember
    {
        [JsonPropertyName("entries_mitgliedsNummer")]
        [JsonRequired]
        public long MemberId { get; init; }
        
        
        [JsonPropertyName("entries_vorname")]
        [JsonRequired]
        public required string FirstName { get; init; }
        
        [JsonPropertyName("entries_nachname")]
        [JsonRequired]
        public required string LastName { get; init; }
        
        [JsonPropertyName("entries_geburtsDatum")]
        [JsonConverter(typeof(NamiDateOnlyConverter))]
        [JsonRequired]
        public DateOnly DateOfBirth { get; init; }
        
        [JsonPropertyName("entries_stufe")]
        [JsonRequired]
        public required string Rank { get; init; }
        
        [JsonPropertyName("entries_geschlecht")]
        [JsonRequired]
        public required string Gender { get; init; }
    }
}