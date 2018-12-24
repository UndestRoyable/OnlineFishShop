using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFishShop.Data.Models;

namespace OnlineFishShop.Data.EntityConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("Comments");

            entity
                .HasOne(c => c.Product)
                .WithMany(p => p.Comments);

            entity
                .Property(x => x.Message)
                .HasDefaultValue("");

            entity
                .Property(x => x.IsEdited)
                .HasDefaultValue(false);
        }
    }
}
