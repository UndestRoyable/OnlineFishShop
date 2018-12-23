using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFishShop.Data.Models
{
    public class Faq
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Answer { get; set; }
    }
}
