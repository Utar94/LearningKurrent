using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, UpdateProductPayload Payload) : IRequest;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IProductRepository _productRepository;

  public UpdateProductCommandHandler(IApplicationContext applicationContext, IProductRepository productRepository)
  {
    _applicationContext = applicationContext;
    _productRepository = productRepository;
  }

  public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
  {
    UpdateProductPayload payload = command.Payload;
    // TODO(fpion): validate

    ProductId id = new(command.Id);
    Product product = await _productRepository.LoadAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

    ProductUpdates updates = new()
  {
      Sku = Sku.TryCreate(payload.Sku),
      DisplayName = payload.DisplayName == null ? null : new Change<DisplayName>(DisplayName.TryCreate(payload.DisplayName.Value)),
      Description = payload.Description == null ? null : new Change<Description>(Description.TryCreate(payload.Description.Value)),
      Price = payload.Price == null ? null : new Change<Price>(payload.Price.Value.HasValue ? new Price(payload.Price.Value.Value) : null),
      PictureUrl = payload.PictureUrl == null ? null : new Change<Url>(Url.TryCreate(payload.PictureUrl.Value))
    };
    product.Update(updates, _applicationContext.ActorId);

    // TODO(fpion): ensure SKU unicity

    await _productRepository.SaveAsync(product, cancellationToken);
  }
}
