using LearningKurrent.Domain;

namespace LearningKurrent.Application.Products.Models;

public record UpdateProductPayload
{
  public string? Sku { get; set; }
  public Change<string>? DisplayName { get; set; }
  public Change<string>? Description { get; set; }
  public Change<decimal>? Price { get; set; }
  public Change<string>? PictureUrl { get; set; }
}
