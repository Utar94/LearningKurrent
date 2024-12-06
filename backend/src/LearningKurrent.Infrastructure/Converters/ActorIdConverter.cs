using LearningKurrent.Domain;

namespace LearningKurrent.Infrastructure.Converters;

internal class ActorIdConverter : JsonConverter<ActorId>
{
  public override ActorId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    string? value = reader.GetString();
    return value == null ? new ActorId() : new(value);
  }

  public override void Write(Utf8JsonWriter writer, ActorId actorId, JsonSerializerOptions options)
  {
    writer.WriteStringValue(actorId.Value);
  }
}
