using LearningKurrent.Domain.Products;

namespace LearningKurrent.Infrastructure.Converters;

internal class PriceConverter : JsonConverter<Price>
{
  public override Price? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    return reader.TryGetDecimal(out decimal value) ? new Price(value) : null;
  }

  public override void Write(Utf8JsonWriter writer, Price price, JsonSerializerOptions options)
  {
    writer.WriteNumberValue(price.Value);
  }
}
