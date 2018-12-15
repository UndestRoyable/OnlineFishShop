using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineFishShop.Data.Models.Checkout;

namespace OnlineFishShop.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Purchase> Purchases { get; set; }
    }
}
