using LearningKurrent.Application.Products.Models;
using MediatR;

namespace LearningKurrent.Application.Products.Commands;

public record UpdateProductCommand(string Id, UpdateProductPayload Payload) : IRequest;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
  public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    return Task.CompletedTask; // TODO(fpion): implement
  }
}
