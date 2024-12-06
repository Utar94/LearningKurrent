using LearningKurrent.Application.Models;

namespace LearningKurrent.Application.Products.Models;

public record SearchProductsPayload : SearchPayload
{
  public new List<ProductSortOption> Sort { get; set; } = [];
}
