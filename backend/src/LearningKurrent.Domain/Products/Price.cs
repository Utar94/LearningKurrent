using FluentValidation;

namespace LearningKurrent.Domain.Products;

public record Price
{
  public decimal Value { get; }

  public Price(decimal value)
  {
    Value = value;
  }

  private class Validator : AbstractValidator<Price>
  {
    public Validator()
    {
      RuleFor(x => x.Value).Price();
    }
  }
}
