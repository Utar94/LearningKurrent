using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products.Models;
using MediatR;

namespace LearningKurrent.Application.Products.Queries;

public record SearchProductsQuery(SearchProductsPayload Payload) : IRequest<SearchResults<ProductModel>>;

internal class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchResults<ProductModel>>
{
  public Task<SearchResults<ProductModel>> Handle(SearchProductsQuery query, CancellationToken cancellationToken)
  {
    SearchResults<ProductModel> results = new();
    return Task.FromResult(results); // TODO(fpion): implement
  }
}
