using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Services.Catalog.Domain.BrandAggregate;

namespace MyStore.Services.Catalog.Repository.TypeConfigurations
{
    public class BrandCategoryTypeConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure(EntityTypeBuilder<BrandCategory> builder)
        {
            builder.ToTable("BrandCategories").HasKey(p => p.Id);
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
