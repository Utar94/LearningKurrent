using LearningKurrent.Domain.Products;
using Logitar;

namespace LearningKurrent.Application.Products;

public class ProductNotFoundException : Exception
{
  private const string ErrorMessage = "The specified product could not be found.";

  public Guid ProductId
  {
    get => (Guid)Data[nameof(ProductId)]!;
    private set => Data[nameof(ProductId)] = value;
  }

  public ProductNotFoundException(ProductId id) : base(BuildMessage(id))
  {
    ProductId = id.ToGuid();
  }

  private static string BuildMessage(ProductId id) => new ErrorMessageBuilder(ErrorMessage)
    .AddData(nameof(ProductId), id.ToGuid())
    .Build();
}
