using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineFishShop.Data.EntityConfigurations;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Data.Models.Checkout;
using OnlineFishShop.Data.Models.Mapping;

namespace OnlineFishShop.Data
{
    public class OnlineFishShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineFishShopDbContext(DbContextOptions<OnlineFishShopDbContext> options)
            :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }

        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }

        public DbSet<Faq> Faqs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProductConfiguration());

            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new RatingConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CategoryProductConfiguration());

            builder.ApplyConfiguration(new BlogPostConfiguration());
            builder.ApplyConfiguration(new BlogCommentConfiguration());

            builder.ApplyConfiguration(new FaqConfiguration());
        }
    }
}
