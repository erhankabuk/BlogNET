using BlogNET.Areas.Admin.Models;
using BlogNET.Areas.Admin.Services;
using BlogNET.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UrlServices _urlService;
        private readonly PhotoService _photoService;

        public PostsController(ApplicationDbContext db, IWebHostEnvironment env, UrlServices urlService ,PhotoService photoService)
        {
            _db = db;
            _env = env;
            _urlService = urlService;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            return View(_db.Posts
                .Include(x => x.Category)
                .Include(x => x.Author)
                .ToList());
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = _db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }            
            _photoService.DeletePhoto(post.PhotoPath);
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return RedirectToAction("Index", new { message = "delete" });
        }
       
        public IActionResult New()
        {
            var vm = new NewPostViewModel()
            {
                Categories = _db.Categories.OrderBy(x => x.Name).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };
            return View(vm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult New(NewPostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post()
                {
                    AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CategoryId = vm.CategoryId.Value,
                    Content = vm.Content,
                    Title = vm.Title,
                    Slug = _urlService.URLFriendly(vm.Slug),
                    PhotoPath =_photoService.SavePhoto(vm.FeaturedImage),
                    isPublished=vm.IsPublished
                };
                _db.Add(post);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            vm.Categories = _db.Categories.OrderBy(x => x.Name).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult GenerateSlug(string text)
        {
            string slug = _urlService.URLFriendly(text);
            return Content(slug);

        }







    }
}
