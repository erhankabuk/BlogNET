using BlogNET.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;

        public CommentsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Comments.Include(x=>x.Author).OrderByDescending(x=>x.CreatedTime).ToList());
        }
    }
}
