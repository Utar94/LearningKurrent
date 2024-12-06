using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using LearningKurrent.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningKurrent.Infrastructure.Configurations;

internal class ProductConfiguration : AggregateConfiguration<ProductEntity>, IEntityTypeConfiguration<ProductEntity>
{
  public override void Configure(EntityTypeBuilder<ProductEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(Db.Products.Table.Table!, Db.Products.Table.Schema);
    builder.HasKey(x => x.ProductId);

    builder.HasIndex(x => x.Id).IsUnique();
    builder.HasIndex(x => x.Sku);
    builder.HasIndex(x => x.SkuNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.Price);

    builder.Property(x => x.Sku).HasMaxLength(Sku.MaximumLength);
    builder.Property(x => x.SkuNormalized).HasMaxLength(Sku.MaximumLength);
    builder.Property(x => x.DisplayName).HasMaxLength(DisplayName.MaximumLength);
    builder.Property(x => x.Description).HasMaxLength(Description.MaximumLength);
    builder.Property(x => x.Price).HasColumnType("money");
    builder.Property(x => x.PictureUrl).HasMaxLength(Url.MaximumLength);
  }
}
