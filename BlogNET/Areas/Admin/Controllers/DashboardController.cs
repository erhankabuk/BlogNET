
using BlogNET.Data;
using BlogNET.Areas.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BlogNET.Areas.Admin.Models;
using BlogNET.Areas.Admin.Controllers;

namespace BlogNET.Areas.Admin.Controllers
{
   
    public class DashboardController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var vm = new DashboardViewModel()
            {
                CategoryCount = _db.Categories.Count(),
                PostCount = _db.Posts.Count(),
                UserCount = _db.Users.Count(),
                CommentCount= _db.Comments.Count()
            };
            return View(vm);
        }
    }
}
