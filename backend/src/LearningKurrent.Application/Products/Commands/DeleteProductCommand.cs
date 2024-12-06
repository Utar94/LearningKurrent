using LearningKurrent.Domain.Products;
using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IProductRepository _productRepository;

  public DeleteProductCommandHandler(IApplicationContext applicationContext, IProductRepository productRepository)
  {
    _applicationContext = applicationContext;
    _productRepository = productRepository;
  }

  public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
  {
    ProductId id = new(command.Id);
    Product product = await _productRepository.LoadAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

    product.Delete(_applicationContext.ActorId);

    await _productRepository.SaveAsync(product, cancellationToken);
  }
}
