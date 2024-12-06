using Logitar;
using System.Diagnostics.CodeAnalysis;

namespace LearningKurrent.Domain;

public readonly struct EventId
{
  public const int MaximumLength = byte.MaxValue;

  private readonly string? _value = null;
  public string Value => _value ?? string.Empty;

  public EventId(Guid value) : this(Convert.ToBase64String(value.ToByteArray()).ToUriSafeBase64())
  {
  }

  public EventId(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new ArgumentException("TODO", nameof(value));
    }

    value = value.Trim();
    if (value.Length > MaximumLength)
    {
      throw new ArgumentOutOfRangeException(nameof(value), "TODO");
    }

    _value = value;
  }

  public static EventId NewId() => new(Guid.NewGuid());

  public Guid ToGuid() => new(Convert.FromBase64String(Value.FromUriSafeBase64()));

  public static bool operator ==(EventId left, EventId right) => left.Equals(right);
  public static bool operator !=(EventId left, EventId right) => !left.Equals(right);

  public override bool Equals([NotNullWhen(true)] object? obj) => obj is EventId id && id.Value == Value;
  public override int GetHashCode() => Value.GetHashCode();
  public override string ToString() => Value;
}
