using MediatR;

namespace LearningKurrent.Domain.Products.Events;

public record ProductDeleted : DomainEvent, IDeleteEvent, INotification;
