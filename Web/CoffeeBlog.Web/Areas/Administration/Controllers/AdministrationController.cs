namespace CoffeeBlog.Web.Areas.Administration.Controllers
{
    using CoffeeBlog.Common;
    using CoffeeBlog.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
