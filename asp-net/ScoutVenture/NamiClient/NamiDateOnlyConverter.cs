using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NamiClient
{
    public class NamiDateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTimeStr = reader.GetString();
            if (DateTime.TryParseExact(dateTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date))
            {
                return DateOnly.FromDateTime(date);
            }

            throw new JsonException($"Invalid date format. Expected format: yyyy-MM-dd HH:mm:ss");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}