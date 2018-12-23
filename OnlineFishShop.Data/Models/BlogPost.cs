using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFishShop.Data.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        public ICollection<BlogComment> Comments { get; set; }
    }
}
