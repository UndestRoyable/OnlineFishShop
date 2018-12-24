using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineFishShop.Data.Models.Checkout
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        public int DeliveryOptionId { get; set; }
        public virtual DeliveryOption DeliveryOption { get; set; }

        public int PaymentOptionId { get; set; }
        public virtual PaymentOption PaymentOption { get; set; }

        public bool IsOnline { get; set; }
    }
}