using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFishShop.Data.Models.Mapping
{
    public class CategoryProduct
    {
        [Key]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Key]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
