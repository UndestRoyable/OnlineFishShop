using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFishShop.Data.Models.Mapping;

namespace OnlineFishShop.Data.EntityConfigurations
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> entity)
        {
            entity.HasKey(bc => new
            {
                bc.ProductId,
                bc.CategoryId,
            });

            entity.HasOne(bc => bc.Category)
                .WithMany(b => b.CategoryProducts)
                .HasForeignKey(bc => bc.CategoryId);

            entity.HasOne(bc => bc.Product)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(bc => bc.ProductId);
        }
    }
}
