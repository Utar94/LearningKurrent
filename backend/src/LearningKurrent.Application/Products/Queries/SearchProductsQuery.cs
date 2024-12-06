using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products.Models;
using MediatR;

namespace LearningKurrent.Application.Products.Queries;

public record SearchProductsQuery(SearchProductsPayload Payload) : IRequest<SearchResults<ProductModel>>;

internal class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchResults<ProductModel>>
{
  private readonly IProductQuerier _productQuerier;

  public SearchProductsQueryHandler(IProductQuerier productQuerier)
  {
    _productQuerier = productQuerier;
  }

  public async Task<SearchResults<ProductModel>> Handle(SearchProductsQuery query, CancellationToken cancellationToken)
  {
    return await _productQuerier.SearchAsync(query.Payload, cancellationToken);
  }
}
