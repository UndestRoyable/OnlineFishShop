using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFishShop.Data.Models.Checkout
{
    public class DeliveryOption
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
