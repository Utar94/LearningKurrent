using FluentValidation;

namespace LearningKurrent.Domain.Products;

public record Description
{
  public const int MaximumLength = ushort.MaxValue;

  public string Value { get; }

  public Description(string value)
  {
    Value = value.Trim();
  }

  public static Description? TryCreate(string? value) => string.IsNullOrWhiteSpace(value) ? null : new(value);

  private class Validator : AbstractValidator<Description>
  {
    public Validator()
    {
      RuleFor(x => x.Value).Description();
    }
  }
}
