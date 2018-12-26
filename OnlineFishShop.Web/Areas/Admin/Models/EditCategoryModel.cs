using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Web.Models.Automapper;

namespace OnlineFishShop.Web.Areas.Admin.Models
{
    public class EditCategoryModel : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
