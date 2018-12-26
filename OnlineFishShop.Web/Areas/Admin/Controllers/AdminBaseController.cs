using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineFishShop.Web.Controllers;
using OnlineFishShop.Web.Infrastructure.Constants;

namespace OnlineFishShop.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.Areas.AdminArea)]
    [Authorize(Roles = WebConstants.RoleNames.AdminRole)]
    public abstract class AdminBaseController : BaseController
    {
    }
}
