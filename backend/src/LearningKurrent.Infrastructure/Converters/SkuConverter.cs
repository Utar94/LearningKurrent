using LearningKurrent.Domain.Products;

namespace LearningKurrent.Infrastructure.Converters;

internal class SkuConverter : JsonConverter<Sku>
{
  public override Sku? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    return Sku.TryCreate(reader.GetString());
  }

  public override void Write(Utf8JsonWriter writer, Sku sku, JsonSerializerOptions options)
  {
    writer.WriteStringValue(sku.Value);
  }
}
