using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStore.Services.Catalog.Domain.BrandAggregate;

namespace MyStore.Services.Catalog.Repository.TypeConfigurations
{
    public class BrandTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands").HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(512);
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasMany(p => p.Categories).WithOne().HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(p => p.Categories).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
