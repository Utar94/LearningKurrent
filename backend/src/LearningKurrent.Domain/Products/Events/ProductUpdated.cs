using MediatR;

namespace LearningKurrent.Domain.Products.Events;

public record ProductUpdated(
  Sku? Sku,
  Change<DisplayName>? DisplayName,
  Change<Description>? Description,
  Change<Price>? Price,
  Change<Url>? PictureUrl) : DomainEvent, INotification;
