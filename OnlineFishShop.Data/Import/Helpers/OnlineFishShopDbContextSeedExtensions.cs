using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Data.Models.Checkout;
using OnlineFishShop.Data.Models.Mapping;

namespace OnlineFishShop.Data.Import.Helpers
{
    public static class OnlineFishShopDbContextSeedExtensions
    {
        public static void EnsureSeedData(this OnlineFishShopDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any()) return;

            //categories to list
            var categoriesToSeed = new Category[]
            {
                new Category()
                {
                    Name = "Fish"
                },
                new Category()
                {
                    Name = "Roe"
                },
                new Category()
                {
                    Name = "Shellfish"
                },
                new Category()
                {
                    Name = "Tunicates"
                }
            };

            //products to list
            var currentDirectory = Path.GetFullPath(@"..\OnlineFishShop.Data\Import\Json\ProductsMock.json");
            string json = File.ReadAllText(currentDirectory);
            var productsToSeed = JsonConvert.DeserializeObject<Product[]>(json);

            //seeding products and categories
            SeedProductsAndCategories(context, categoriesToSeed, productsToSeed);
            SeedRatings(context);
            SeedComments(context);
            SeedDeliveryOptions(context);
            SeedPaymentOptions(context);
        }

        private static void SeedPaymentOptions(OnlineFishShopDbContext context)
        {
            if (context.PaymentOptions.Any()) return;

            var currentDirectory = Path.GetFullPath(@"..\OnlineFishShop.Data\Import\Json\PaymentOptionsMock.json");
            string json = File.ReadAllText(currentDirectory);
            var optionsToSeed = JsonConvert.DeserializeObject<List<PaymentOption>>(json);

            context.PaymentOptions.AddRange(optionsToSeed);
            context.SaveChanges();
        }

        private static void SeedDeliveryOptions(OnlineFishShopDbContext context)
        {
            if (context.DeliveryOptions.Any()) return;

            var currentDirectory = Path.GetFullPath(@"..\OnlineFishShop.Data\Import\Json\DeliveryOptionsMock.json");
            string json = File.ReadAllText(currentDirectory);
            var optionsToSeed = JsonConvert.DeserializeObject<List<DeliveryOption>>(json);

            context.DeliveryOptions.AddRange(optionsToSeed);
            context.SaveChanges();
        }

        private static void SeedRatings(OnlineFishShopDbContext context)
        {
            if (context.Ratings.Any()) return;

            var currentDirectory = Path.GetFullPath(@"..\OnlineFishShop.Data\Import\Json\RatingsMock.json");
            string json = File.ReadAllText(currentDirectory);
            var ratingsToSeed = JsonConvert.DeserializeObject<List<Rating>>(json);

            Random r = new Random();

            var products = context.Products.ToList();
            var user = context.Users.First(); //sequence users contains no elements

            foreach (var rating in ratingsToSeed)
            {
                rating.Product = products[r.Next(1, 25)];
                rating.User = user;
            }

            context.Ratings.AddRange(ratingsToSeed);
            context.SaveChanges();
        }

        private static void SeedComments(OnlineFishShopDbContext context)
        {
            if (context.Comments.Any()) return;

            var currentDirectory = Path.GetFullPath(@"..\OnlineFishShop.Data\Import\Json\CommentsMock.json");
            string json = File.ReadAllText(currentDirectory);

            var dateTimeFormat = "dd/mm/yyyy";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = dateTimeFormat };

            var commentsToSeed = JsonConvert.DeserializeObject<List<Comment>>(json, dateTimeConverter);

            Random r = new Random();

            var products = context.Products.ToList();
            var user = context.Users.First();

            //attach product and user to comments
            foreach (var comment in commentsToSeed)
            {
                comment.Product = products[r.Next(1, 25)];
                comment.User = user;
            }

            context.AddRange(commentsToSeed);
            context.SaveChanges();
        }

        private static void SeedProductsAndCategories(OnlineFishShopDbContext context, Category[] categoriesToSeed,
            Product[] productsToSeed)
        {
            if (!context.Products.Any())
            {
                Random r = new Random();
                List<CategoryProduct> categoryProducts = new List<CategoryProduct>();

                foreach (var product in productsToSeed)
                {
                    categoryProducts.Add(new CategoryProduct()
                    {
                        Product = product,
                        Category = categoriesToSeed[r.Next(0, categoriesToSeed.Length)]
                    });
                }

                context.AddRange(categoryProducts);
                context.SaveChanges();
            }
        }
    }
}
