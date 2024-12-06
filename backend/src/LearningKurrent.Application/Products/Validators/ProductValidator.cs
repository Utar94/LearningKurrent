﻿using FluentValidation;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain;

namespace LearningKurrent.Application.Products.Validators;

internal class ProductValidator : AbstractValidator<ProductPayload>
{
  public ProductValidator()
  {
    RuleFor(x => x.Sku).Sku();
    When(x => !string.IsNullOrWhiteSpace(x.DisplayName), () => RuleFor(x => x.DisplayName!).DisplayName());
    When(x => !string.IsNullOrWhiteSpace(x.Description), () => RuleFor(x => x.Description!).Description());
    When(x => x.Price.HasValue, () => RuleFor(x => x.Price!.Value).Price());
    When(x => !string.IsNullOrWhiteSpace(x.PictureUrl), () => RuleFor(x => x.PictureUrl!).Url());
  }
}
