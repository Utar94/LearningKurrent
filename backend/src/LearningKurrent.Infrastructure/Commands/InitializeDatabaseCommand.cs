using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearningKurrent.Infrastructure.Commands;

public record InitializeDatabaseCommand : IRequest;

internal class InitializeDatabaseCommandHandler : IRequestHandler<InitializeDatabaseCommand>
{
  private readonly CommerceContext _context;

  public InitializeDatabaseCommandHandler(CommerceContext context)
  {
    _context = context;
  }

  public async Task Handle(InitializeDatabaseCommand _, CancellationToken cancellationToken)
  {
    await _context.Database.MigrateAsync(cancellationToken);
  }
}
