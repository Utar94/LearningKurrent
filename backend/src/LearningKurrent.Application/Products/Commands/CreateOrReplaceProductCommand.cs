using FluentValidation;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Application.Products.Validators;
using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record CreateOrReplaceProductCommand(Guid? Id, ProductPayload Payload, long? Version) : IRequest;

internal class CreateOrReplaceProductCommandHandler : IRequestHandler<CreateOrReplaceProductCommand>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IProductRepository _productRepository;

  public CreateOrReplaceProductCommandHandler(IApplicationContext applicationContext, IProductRepository productRepository)
  {
    _applicationContext = applicationContext;
    _productRepository = productRepository;
  }

  public async Task Handle(CreateOrReplaceProductCommand command, CancellationToken cancellationToken)
  {
    ProductPayload payload = command.Payload;
    new ProductValidator().ValidateAndThrow(payload);

    Sku sku = new(payload.Sku);

    ProductId? id = command.Id.HasValue ? new(command.Id.Value) : null;
    Product? product = id.HasValue ? await _productRepository.LoadAsync(id.Value, cancellationToken) : null;
    if (product == null)
    {
      if (command.Version.HasValue)
      {
        throw new ProductNotFoundException(id!.Value);
      }

      product = new Product(sku, _applicationContext.ActorId, id);
    }

    Product reference = (command.Version.HasValue
      ? await _productRepository.LoadAsync(product.Id, command.Version.Value, cancellationToken)
      : null) ?? product;

    ProductUpdates updates = new();
    if (reference.Sku != sku)
    {
      updates.Sku = sku;
    }
    DisplayName? displayName = DisplayName.TryCreate(payload.DisplayName);
    if (reference.DisplayName != displayName)
    {
      updates.DisplayName = new Change<DisplayName>(displayName);
    }
    Description? description = Description.TryCreate(payload.Description);
    if (reference.Description != description)
    {
      updates.Description = new Change<Description>(description);
    }
    Price? price = payload.Price.HasValue ? new(payload.Price.Value) : null;
    if (reference.Price != price)
    {
      updates.Price = new Change<Price>(price);
    }
    Url? pictureUrl = Url.TryCreate(payload.PictureUrl);
    if (reference.PictureUrl != pictureUrl)
    {
      updates.PictureUrl = new Change<Url>(pictureUrl);
    }
    product.Update(updates, _applicationContext.ActorId);

    // TODO(fpion): ensure SKU unicity

    await _productRepository.SaveAsync(product, cancellationToken);
  }
}
