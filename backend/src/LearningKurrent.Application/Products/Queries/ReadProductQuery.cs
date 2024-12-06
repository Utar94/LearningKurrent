using LearningKurrent.Application.Products.Models;
using MediatR;

namespace LearningKurrent.Application.Products.Queries;

public record ReadProductQuery(string? Id, string? Sku) : IRequest<ProductModel?>;

internal class ReadProductQueryHandler : IRequestHandler<ReadProductQuery, ProductModel?>
{
  public Task<ProductModel?> Handle(ReadProductQuery query, CancellationToken cancellationToken)
  {
    return Task.FromResult<ProductModel?>(null); // TODO(fpion): implement
  }
}
