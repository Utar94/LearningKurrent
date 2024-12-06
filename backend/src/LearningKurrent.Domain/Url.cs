﻿using FluentValidation;

namespace LearningKurrent.Domain;

public record Url
{
  public const int MaximumLength = 2048;

  public string Value { get; }

  public Url(string value)
  {
    Value = value.Trim();
  }

  private class Validator : AbstractValidator<Url>
  {
    public Validator()
    {
      RuleFor(x => x.Value).Url();
    }
  }
}