using Instagram.Web.Areas.Client.Models.Home;
using Instagram.Web.Data;
using Instagram.Web.Entities;
using Instagram.Web.Extensions;
using Instagram.Web.Filters;
using Instagram.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [AllowAnonymous(ControllerName = nameof(HomeController), Namespace = "Instagram.Web.Areas.Client.Controllers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            var userFromDb = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (userFromDb == null)
                ModelState.AddModelError("error", "Username or password is incorrect");

            var vm = new LoginViewModel();
            vm.User = user;

            if (ModelState.IsValid)
            {
                var token = Guid.NewGuid().ToString();

                userFromDb.Token = token;

                _context.SaveChanges();

                Response.Cookies.Set("token", token, 1440);

                return RedirectToAction("Index",
                                   userFromDb is RegularUser ? "Post" : "Admin");
            }

            return View(vm);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegularUser user)
        {
            _context.RegularUsers.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Login));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
