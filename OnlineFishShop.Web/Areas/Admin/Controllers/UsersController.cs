using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Web.Models;

namespace OnlineFishShop.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServiceProvider serviceProvider;

        public UsersController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
        {
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            var usersWithRoles = new List<UserWithRolesModel>();
            var users = this.userManager.Users;

            foreach (var user in users)
                usersWithRoles.Add(new UserWithRolesModel()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToArray()
                });

            return View(usersWithRoles);
        }

        public IActionResult Edit(string username)
        {
            var user = this.userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            if (user == null)
            {
                return NotFound();
            }

            this.ViewData["ReturnUrl"] = "/Admin/Users";

            var roles = this.serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>()
                .Roles
                .Select(x => x.Name);

            this.ViewData["AppRoles"] = roles;

            return View(new UserWithRolesModel()
            {
                Username = user.UserName,
                Email = user.Email,
                Roles = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToArray()
            });
        }

        public async Task<IActionResult> Delete(string username)
        {
            //delete user
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeUserRole(string username, string newRole)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var isInRole = this.userManager.IsInRoleAsync(user, newRole).GetAwaiter().GetResult();

            if (isInRole)
            {
                //remove role
                await this.userManager.RemoveFromRoleAsync(user, newRole);
                return RedirectToAction(nameof(Edit), new { username = username });
            }

            //add role
            await this.userManager.AddToRoleAsync(user, newRole);
            return RedirectToAction(nameof(Edit), new { username = username });
        }
    }
}
