using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using LearningKurrent.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningKurrent.Infrastructure.Queriers;

internal class ProductQuerier : IProductQuerier
{
  private readonly DbSet<ProductEntity> _products;

  public ProductQuerier(CommerceContext context)
  {
    _products = context.Products;
  }

  public async Task<ProductModel?> ReadAsync(Product product, CancellationToken cancellationToken)
  {
    return await ReadAsync(product.Id, cancellationToken);
  }
  public async Task<ProductModel?> ReadAsync(ProductId id, CancellationToken cancellationToken)
  {
    return await ReadAsync(id.ToGuid(), cancellationToken);
  }
  public async Task<ProductModel?> ReadAsync(Guid id, CancellationToken cancellationToken)
  {
    ProductEntity? product = await _products.AsNoTracking()
      .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    return product == null ? null : await MapAsync(product, cancellationToken);
  }
  public async Task<ProductModel?> ReadAsync(string sku, CancellationToken cancellationToken)
  {
    string skuNormalized = Db.Helper.Normalize(sku);

    ProductEntity? product = await _products.AsNoTracking()
      .SingleOrDefaultAsync(x => x.SkuNormalized == skuNormalized, cancellationToken);

    return product == null ? null : await MapAsync(product, cancellationToken);
  }

  public Task<SearchResults<ProductModel>> SearchAsync(SearchProductsPayload payload, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }

  private async Task<ProductModel> MapAsync(ProductEntity product, CancellationToken cancellationToken)
  {
    return (await MapAsync([product], cancellationToken)).Single();
  }
  private Task<IReadOnlyCollection<ProductModel>> MapAsync(IEnumerable<ProductEntity> products, CancellationToken cancellationToken)
  {
    IEnumerable<ActorId> actorIds = products.SelectMany(product => product.GetActorIds());
    ActorModel[] actors = []; // TODO(fpion): implement
    Mapper mapper = new(actors);

    ProductModel[] p = products.Select(mapper.ToProduct).ToArray();
    return Task.FromResult<IReadOnlyCollection<ProductModel>>(p);
  }
}
