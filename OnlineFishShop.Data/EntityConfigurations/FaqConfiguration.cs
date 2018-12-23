using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFishShop.Data.Models;

namespace OnlineFishShop.Data.EntityConfigurations
{
    public class FaqConfiguration : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> entity)
        {
            entity.ToTable("Faqs");

            entity
                .Property(x => x.Answer)
                .HasDefaultValue("");

            entity
                .Property(x => x.Title)
                .HasDefaultValue("");
        }
    }
}
