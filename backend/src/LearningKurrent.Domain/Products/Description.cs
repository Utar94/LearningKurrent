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

  private class Validator : AbstractValidator<Description>
  {
    public Validator()
    {
      RuleFor(x => x.Value).Description();
    }
  }
}
