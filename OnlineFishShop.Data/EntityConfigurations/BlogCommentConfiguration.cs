using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFishShop.Data.Models;

namespace OnlineFishShop.Data.EntityConfigurations
{
    public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> entity)
        {
            entity.ToTable("BlogComments");

            entity
                .HasOne(c => c.BlogPost)
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
