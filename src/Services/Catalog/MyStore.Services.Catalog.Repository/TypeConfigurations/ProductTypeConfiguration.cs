using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Services.Catalog.Domain;

namespace MyStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(p => p.Id);

            builder.Property(p => p.Sku).HasMaxLength(128);
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,4)").IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(512);
            
            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Restrict).IsRequired();

            builder.HasMany(p => p.Categories).WithOne().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(p => p.Categories).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
