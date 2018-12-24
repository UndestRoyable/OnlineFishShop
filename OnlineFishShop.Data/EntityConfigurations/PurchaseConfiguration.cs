using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFishShop.Data.Models.Checkout;

namespace OnlineFishShop.Data.EntityConfigurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> entity)
        {
            entity.ToTable("Purchases");

            entity.HasOne(c => c.DeliveryOption);

            entity.HasOne(c => c.PaymentOption);
        }
    }
}
