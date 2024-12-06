using MediatR;

namespace LearningKurrent.Domain.Products.Events;

public record ProductCreated(Sku Sku) : DomainEvent, INotification;
