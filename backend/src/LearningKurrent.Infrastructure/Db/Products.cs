using LearningKurrent.Infrastructure.Entities;
using Logitar.Data;

namespace LearningKurrent.Infrastructure.Db;

internal static class Products
{
  public static readonly TableId Table = new(nameof(CommerceContext.Products));

  public static readonly ColumnId Description = new(nameof(ProductEntity.Description), Table);
  public static readonly ColumnId DisplayName = new(nameof(ProductEntity.DisplayName), Table);
  public static readonly ColumnId Id = new(nameof(ProductEntity.Id), Table);
  public static readonly ColumnId PictureUrl = new(nameof(ProductEntity.PictureUrl), Table);
  public static readonly ColumnId Price = new(nameof(ProductEntity.Price), Table);
  public static readonly ColumnId ProductId = new(nameof(ProductEntity.ProductId), Table);
  public static readonly ColumnId Sku = new(nameof(ProductEntity.Sku), Table);
  public static readonly ColumnId SkuNormalized = new(nameof(ProductEntity.SkuNormalized), Table);
}
