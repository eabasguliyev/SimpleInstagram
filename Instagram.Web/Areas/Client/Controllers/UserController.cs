using Instagram.Web.Areas.Client.Models;
using Instagram.Web.Areas.Client.Models.User;
using Instagram.Web.Data;
using Instagram.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Instagram.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [RequireAuthorization(ControllerName = nameof(UserController), Namespace = "Instagram.Web.Areas.Client.Controllers", UserType = UserType.User)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            _context.RegularUsers.FirstOrDefault(u => u.Token == HttpContext.Request.Cookies["token"]).Token = null;
            _context.SaveChanges();
            Response.Cookies.Delete("token");
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            //_context.Users.Add(new Entities.RegularUser
            //{
            //    FirstName = "Abil",
            //    LastName = "Allahyarov",
            //    Username = "abil",
            //    Email = "abil@gmail.com",
            //    Password = "abil"
            //});

            //_context.Users.Add(new Entities.RegularUser
            //{
            //    FirstName = "Abil",
            //    LastName = "Memmedov",
            //    Username = "abil2005",
            //    Email = "abil2@gmail.com",
            //    Password = "abil"
            //});

            //_context.Users.Add(new Entities.RegularUser
            //{
            //    FirstName = "Elvin",
            //    LastName = "Cavadov",
            //    Username = "elvin",
            //    Email = "elvin@gmail.com",
            //    Password = "elvin"
            //});

            //_context.Users.Add(new Entities.RegularUser
            //{
            //    FirstName = "Arif",
            //    LastName = "Baghirli",
            //    Username = "arif",
            //    Email = "arif@gmail.com",
            //    Password = "arif"
            //});

            //_context.SaveChanges();

            return View();
        }

        public IActionResult Search(string name)
        {
            var searchResult = _context.RegularUsers
                                        .Where(u => 
                                            (u.FirstName + " " + u.LastName)
                                                   .Contains(name))
                                        .ToList();

            var vm = new SearchViewModel();
            vm.Results = searchResult;
            vm.LoggedUser = _context.RegularUsers.FirstOrDefault(u => u.Token == Request.Cookies["token"]);
            return View(vm);
        }

        public IActionResult Profile(string username)
        {
            var user = _context.RegularUsers.FirstOrDefault(u => u.Username == username);
            var vm = new ProfileViewModel();

            vm.User = user;
            vm.Followers = _context.RegularUsers.Where(u => u.Follows.
                    Any(r => r.ToId == user.Id && r.Status == Entities.FriendshipRequestEnum.Approved)).ToList();

            vm.Follows = _context.RegularUsers.Where(u => u.Followers
                                    .Any(r => r.FromId == user.Id &&
                                    r.Status == Entities.FriendshipRequestEnum.Approved)).ToList();

            vm.Posts = _context.Posts.Where(p => p.PublisherId == user.Id).Include(p => p.PostLikes).ToList();
            vm.LoggedUser = _context.RegularUsers.FirstOrDefault(u => u.Token == Request.Cookies["token"]);
            return View(vm);
        }


        public IActionResult Follow(int id)
        {
            var target = _context.RegularUsers.FirstOrDefault(u => u.Id == id);
            var token = HttpContext.Request.Cookies["token"];

            var from = _context.RegularUsers.FirstOrDefault(u => u.Token == token);

            _context.Friendships.Add(new Entities.Friendship
            {
                FromId = from.Id,
                ToId = target.Id,
                Status = Entities.FriendshipRequestEnum.Approved
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Profile), new { username = target.Username });
        }

        public IActionResult Unfollow(int id)
        {
            var target = _context.RegularUsers.FirstOrDefault(u => u.Id == id);
            var from = _context.RegularUsers.FirstOrDefault(u => u.Token == HttpContext.Request.Cookies["token"]);

            var friendShip = _context.Friendships.FirstOrDefault(u => u.FromId == from.Id && u.ToId == target.Id);

            _context.Friendships.Remove(friendShip);
            _context.SaveChanges();

            return RedirectToAction(nameof(Profile), new { username = target.Username });
        }
    }
}
