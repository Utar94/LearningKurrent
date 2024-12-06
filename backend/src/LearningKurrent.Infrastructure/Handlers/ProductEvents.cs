using LearningKurrent.Domain.Products.Events;
using LearningKurrent.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningKurrent.Infrastructure.Handlers;

internal class ProductEvents : INotificationHandler<ProductCreated>, INotificationHandler<ProductDeleted>, INotificationHandler<ProductUpdated>
{
  private readonly CommerceContext _context;

  public ProductEvents(CommerceContext context)
  {
    _context = context;
  }

  public async Task Handle(ProductCreated @event, CancellationToken cancellationToken)
  {
    ProductEntity? product = await _context.Products.AsNoTracking()
      .SingleOrDefaultAsync(x => x.Id == @event.AggregateId.ToGuid(), cancellationToken);
    if (product == null)
    {
      product = new(@event);

      _context.Products.Add(product);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task Handle(ProductDeleted @event, CancellationToken cancellationToken)
  {
    ProductEntity? product = await _context.Products
      .SingleOrDefaultAsync(x => x.Id == @event.AggregateId.ToGuid(), cancellationToken);
    if (product != null)
    {
      _context.Products.Remove(product);

      await _context.SaveChangesAsync(cancellationToken);
    }
  }

  public async Task Handle(ProductUpdated @event, CancellationToken cancellationToken)
  {
    Guid id = @event.AggregateId.ToGuid();
    ProductEntity product = await _context.Products
      .SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
      ?? throw new InvalidOperationException($"The product entity 'Id={id}' could not be found.");

    product.Update(@event);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
