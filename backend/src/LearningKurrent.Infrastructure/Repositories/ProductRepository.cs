using LearningKurrent.Application.Products;
using LearningKurrent.Domain.Products;

namespace LearningKurrent.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
  public async Task<Product?> LoadAsync(ProductId id, CancellationToken cancellationToken)
  {
    return await LoadAsync(id, version: null, cancellationToken);
  }
  public Task<Product?> LoadAsync(ProductId id, long? version, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }

  public async Task SaveAsync(Product product, CancellationToken cancellationToken)
  {
    await SaveAsync([product], cancellationToken);
  }
  public Task SaveAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }
}
