namespace LearningKurrent.Domain.Products;

public readonly struct ProductId
{
  public AggregateId AggregateId { get; }
  public string Value => AggregateId.Value;

  public ProductId(AggregateId aggregateId)
  {
    AggregateId = aggregateId;
  }

  public ProductId(Guid value)
  {
    AggregateId = new(value);
  }

  public ProductId(string value)
  {
    AggregateId = new(value);
  }

  public static ProductId NewId() => new(AggregateId.NewId());

  public Guid ToGuid() => AggregateId.ToGuid();

  public static bool operator ==(ProductId left, ProductId right) => left.Equals(right);
  public static bool operator !=(ProductId left, ProductId right) => !left.Equals(right);

  public override bool Equals([NotNullWhen(true)] object? obj) => obj is ProductId id && id.Value == Value;
  public override int GetHashCode() => Value.GetHashCode();
  public override string ToString() => Value;
}
