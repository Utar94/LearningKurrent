using LearningKurrent.Application.Models;

namespace LearningKurrent.Application.Products.Models;

public record ProductSortOption : SortOption
{
  public new ProductSort Field
  {
    get => Enum.Parse<ProductSort>(base.Field);
    set => base.Field = value.ToString();
  }

  public ProductSortOption() : base()
  {
  }

  public ProductSortOption(ProductSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
  }
}
