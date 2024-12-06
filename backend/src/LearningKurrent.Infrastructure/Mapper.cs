using LearningKurrent.Application.Models;
using LearningKurrent.Application.Products.Models;
using LearningKurrent.Domain;
using LearningKurrent.Infrastructure.Entities;
using Logitar;

namespace LearningKurrent.Infrastructure;

internal class Mapper
{
  private readonly Dictionary<ActorId, ActorModel> _actors;
  private readonly ActorModel _system = ActorModel.System;

  public Mapper()
  {
    _actors = [];
  }

  public Mapper(IEnumerable<ActorModel> actors) : this()
  {
    foreach (ActorModel actor in actors)
    {
      ActorId actorId = new(actor.Id);
      _actors[actorId] = actor;
    }
  }

  public ProductModel ToProduct(ProductEntity source)
  {
    ProductModel destination = new()
    {
      Id = source.Id,
      Sku = source.Sku,
      DisplayName = source.DisplayName,
      Description = source.Description,
      Price = source.Price,
      PictureUrl = source.PictureUrl
    };

    MapAggregate(source, destination);

    return destination;
  }

  private void MapAggregate(AggregateEntity source, AggregateModel destination)
  {
    destination.Id = source.AggregateId;
    destination.Version = source.Version;

    destination.CreatedBy = source.CreatedBy == null ? _system : FindActor(source.CreatedBy);
    destination.CreatedOn = source.CreatedOn.AsUniversalTime();

    destination.UpdatedBy = source.UpdatedBy == null ? _system : FindActor(source.UpdatedBy);
    destination.UpdatedOn = source.UpdatedOn.AsUniversalTime();
  }
  private ActorModel FindActor(string id) => FindActor(new ActorId(id));
  private ActorModel FindActor(ActorId id) => _actors.TryGetValue(id, out ActorModel? actor) ? actor : _system;
}
