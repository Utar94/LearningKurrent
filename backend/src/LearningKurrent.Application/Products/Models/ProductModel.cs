using LearningKurrent.Application.Models;

namespace LearningKurrent.Application.Products.Models;

public class ProductModel : AggregateModel
{
  public string Sku { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
  public string? Description { get; set; }
  public decimal? Price { get; set; }
  public string? PictureUrl { get; set; }
}
