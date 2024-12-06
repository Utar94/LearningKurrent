using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record DeleteProductCommand(string Id) : IRequest;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
  public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
  {
    return Task.CompletedTask; // TODO(fpion): implement
  }
}
