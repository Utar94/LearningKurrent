namespace LearningKurrent.Domain.Products;

public record ProductUpdates
{
  public Sku? Sku { get; set; }
  public Change<DisplayName>? DisplayName { get; set; }
  public Change<Description>? Description { get; set; }
  public Change<Price>? Price { get; set; }
  public Change<Url>? PictureUrl { get; set; }
}
