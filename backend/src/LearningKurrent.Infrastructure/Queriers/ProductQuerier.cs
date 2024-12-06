using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain.Products;

namespace LearningKurrent.Infrastructure.Queriers;

internal class ProductQuerier : IProductQuerier
{
  public async Task<ProductModel?> ReadAsync(Product product, CancellationToken cancellationToken)
  {
    return await ReadAsync(product.Id, cancellationToken);
  }
  public async Task<ProductModel?> ReadAsync(ProductId id, CancellationToken cancellationToken)
  {
    return await ReadAsync(id.ToGuid(), cancellationToken);
  }
  public Task<ProductModel?> ReadAsync(Guid id, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }
  public Task<ProductModel?> ReadAsync(string sku, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }

  public Task<SearchResults<ProductModel>> SearchAsync(SearchProductsPayload payload, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }
}
