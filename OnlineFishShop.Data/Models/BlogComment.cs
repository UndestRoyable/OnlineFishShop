﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFishShop.Data.Models
{
    public class BlogComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/YYYY HH:mm:ss}")]
        public DateTime DateAdded { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMMM/YYYY HH:mm:ss}")]
        public DateTime? DateEdited { get; set; }

        [Required]
        public bool IsEdited { get; set; }
    }
}
