using LearningKurrent.Domain;

namespace LearningKurrent.Infrastructure.Converters;

internal class AggregateIdConverter : JsonConverter<AggregateId>
{
  public override AggregateId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    return value == null ? new AggregateId() : new(value);
  }

  public override void Write(Utf8JsonWriter writer, AggregateId aggregateId, JsonSerializerOptions options)
  {
    writer.WriteStringValue(aggregateId.Value);
  }
}
