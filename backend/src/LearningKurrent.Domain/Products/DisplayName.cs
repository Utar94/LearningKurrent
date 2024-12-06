using FluentValidation;

namespace LearningKurrent.Domain.Products;

public record DisplayName
{
  public const int MaximumLength = byte.MaxValue;

  public string Value { get; }

  public DisplayName(string value)
  {
    Value = value.Trim();
  }

  private class Validator : AbstractValidator<DisplayName>
  {
    public Validator()
    {
      RuleFor(x => x.Value).DisplayName();
    }
  }
}
