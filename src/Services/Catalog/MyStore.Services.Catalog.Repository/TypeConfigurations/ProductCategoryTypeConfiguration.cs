using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Services.Catalog.Domain.ProductAggregate;

namespace MyStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ProductCategoryTypeConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories").HasKey(p => p.Id);

            builder.Property(p => p.CreationDate).IsRequired();
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
