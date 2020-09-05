using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrackerApplication.Domain
{
    public class MMDashDDDashYYYYDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DATE_TIME_FORMAT = "MM-dd-yyyy HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture));
        }
    }
}
