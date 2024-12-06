using FluentValidation;

namespace LearningKurrent.Domain.Products;

public record Sku
{
  public const int MaximumLength = 32;

  public string Value { get; }

  public Sku(string value)
  {
    Value = value.Trim();
  }

  public static Sku? TryCreate(string? value) => string.IsNullOrWhiteSpace(value) ? null : new(value);

  private class Validator : AbstractValidator<Sku>
  {
    public Validator()
    {
      RuleFor(x => x.Value).Sku();
    }
  }
}
