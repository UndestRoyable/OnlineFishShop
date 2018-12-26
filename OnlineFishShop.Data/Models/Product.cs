using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineFishShop.Data.EntityConfigurations.ValidationAttributes;
using OnlineFishShop.Data.Models.Mapping;

namespace OnlineFishShop.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(25)]
        public string Make { get; set; }

        public string Description { get; set; }

        [Required]
        [StockQuantity]
        public byte Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsInSale { get; set; }

        public decimal? SalePrice { get; set; }

        public string ThumbnailSource { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new HashSet<CategoryProduct>();
    }
}
