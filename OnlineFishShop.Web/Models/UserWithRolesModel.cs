using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFishShop.Web.Models
{
    public class UserWithRolesModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }
    }
}
