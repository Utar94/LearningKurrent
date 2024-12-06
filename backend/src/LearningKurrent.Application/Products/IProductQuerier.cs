using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain.Products;

namespace LearningKurrent.Application.Products;

public interface IProductQuerier
{
  Task<ProductModel> ReadAsync(Product product, CancellationToken cancellationToken = default);
  Task<ProductModel?> ReadAsync(ProductId id, CancellationToken cancellationToken = default);
  Task<ProductModel?> ReadAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ProductModel?> ReadAsync(string sku, CancellationToken cancellationToken = default);

  Task<SearchResults<ProductModel>> SearchAsync(SearchProductsPayload payload, CancellationToken cancellationToken = default);
}
