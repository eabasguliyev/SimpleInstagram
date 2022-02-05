using Instagram.Web.Data;
using Instagram.Web.Entities;
using Instagram.Web.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Instagram.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [RequireAuthorization(ControllerName = nameof(PostController), 
        Namespace = "Instagram.Web.Areas.Client.Controllers", 
        UserType = UserType.User)]
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;

        public PostController(IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {
            var vm = new Models.Post.IndexViewModel();

            var user = _context.RegularUsers.FirstOrDefault(u => u.Token == Request.Cookies["token"]);

            vm.Posts = (from post in _context.Posts
                        join friendship in _context.Friendships on user.Id equals friendship.FromId
                        where friendship.ToId == post.PublisherId
                        select post).Include(p => p.PostLikes).Include(p => p.Publisher).ToList();

            vm.LoggedUser = user;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Post post, IFormFile postImage)
        {
            var wwwRoot = _hostEnvironment.WebRootPath;

            var imageName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRoot, @"images\posts\");
            var extension = Path.GetExtension(postImage.FileName);

            using(var fileStream = new FileStream(Path.Combine(uploads, imageName + extension), FileMode.Create))
            {
                postImage.CopyTo(fileStream);
            }

            post.ImageUrl = @$"\images\posts\{imageName + extension}";

            var user = _context.RegularUsers.FirstOrDefault(u => u.Token == Request.Cookies["token"]);

            post.PublisherId = user.Id;
            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
