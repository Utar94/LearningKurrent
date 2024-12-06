using LearningKurrent.Domain.Products.Events;

namespace LearningKurrent.Domain.Products;

public class Product : AggregateRoot
{
  public new ProductId Id => new(base.Id);

  private Sku? _sku = null;
  public Sku Sku => _sku ?? throw new InvalidOperationException($"The {nameof(Sku)} has not been initialized yet.");
  public DisplayName? DisplayName { get; private set; }
  public Description? Description { get; private set; }
  public Price? Price { get; private set; }
  public Url? PictureUrl { get; private set; }

  public Product() : base()
  {
  }

  public Product(Sku sku, ActorId? actorId, ProductId? id = null) : base((id ?? ProductId.NewId()).AggregateId)
  {
    Raise(new ProductCreated(sku), actorId);
  }
  protected virtual void Handle(ProductCreated @event)
  {
    _sku = @event.Sku;
  }

  public void Delete(ActorId? actorId = null)
  {
    if (!IsDeleted)
    {
      Raise(new ProductDeleted(), actorId);
    }
  }

  public void Update(ProductUpdates updates, ActorId? actorId)
  {
    ProductUpdates delta = new();
    if (updates.Sku != null && updates.Sku != Sku)
    {
      delta.Sku = updates.Sku;
    }
    if (updates.DisplayName != null && updates.DisplayName.Value != DisplayName)
    {
      delta.DisplayName = updates.DisplayName;
    }
    if (updates.Description != null && updates.Description.Value != Description)
    {
      delta.Description = updates.Description;
    }
    if (updates.Price != null && updates.Price.Value != Price)
    {
      delta.Price = updates.Price;
    }
    if (updates.PictureUrl != null && updates.PictureUrl.Value != PictureUrl)
    {
      delta.PictureUrl = updates.PictureUrl;
    }
    if (delta.Sku != null || delta.DisplayName != null || delta.Description != null || delta.Price != null || delta.PictureUrl != null)
    {
      Raise(new ProductUpdated(delta.Sku, delta.DisplayName, delta.Description, delta.Price, delta.PictureUrl), actorId);
    }
  }
  protected virtual void Handle(ProductUpdated @event)
  {
    if (@event.Sku != null)
    {
      _sku = @event.Sku;
    }
    if (@event.DisplayName != null)
    {
      DisplayName = @event.DisplayName.Value;
    }
    if (@event.Description != null)
    {
      Description = @event.Description.Value;
    }
    if (@event.Price != null)
    {
      Price = @event.Price.Value;
    }
    if (@event.PictureUrl != null)
    {
      PictureUrl = @event.PictureUrl.Value;
    }
  }

  public override string ToString() => $"{DisplayName?.Value ?? Sku.Value} | {base.ToString()}";
}
