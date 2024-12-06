using LearningKurrent.Application.Products.Models;
using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record CreateOrReplaceProductCommand(string? Id, ProductPayload Payload, long? Version) : IRequest;

internal class CreateOrReplaceProductCommandHandler : IRequestHandler<CreateOrReplaceProductCommand>
{
  public Task Handle(CreateOrReplaceProductCommand request, CancellationToken cancellationToken)
  {
    return Task.CompletedTask; // TODO(fpion): implement
  }
}
