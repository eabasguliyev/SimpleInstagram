using Instagram.Web.Data;
using Instagram.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [RequireAuthorization(ControllerName = nameof(AdminController),
        Namespace = "Instagram.Web.Areas.Admin.Controllers",
        UserType = UserType.Admin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
