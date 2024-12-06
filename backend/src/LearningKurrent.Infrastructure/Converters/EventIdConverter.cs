using LearningKurrent.Domain;

namespace LearningKurrent.Infrastructure.Converters;

internal class EventIdConverter : JsonConverter<EventId>
{
  public override EventId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    return value == null ? new EventId() : new(value);
  }

  public override void Write(Utf8JsonWriter writer, EventId eventId, JsonSerializerOptions options)
  {
    writer.WriteStringValue(eventId.Value);
  }
}
