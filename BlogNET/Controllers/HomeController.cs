using BlogNET.Data;
using BlogNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private const int POSTS_PER_PAGE = 3;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("c/{slug}")]
        [Route("")]
        public IActionResult Index(string slug, string q, int pn = 1)
        {
            ViewBag.Slug = slug;
            IQueryable<Post> posts = _context.Posts.Where(x => x.isPublished);
            Category category = null;

            if (!string.IsNullOrEmpty(q))
            {
                posts = posts.Where(x => x.Title.Contains(q) || x.Content.Contains(q));
            }

            if (!string.IsNullOrEmpty(slug))
            {
                posts = posts.Where(x => x.Category.Slug == slug);
                category = _context.Categories.FirstOrDefault(x => x.Slug == slug);
            }
            int totalItems = posts.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalItems / POSTS_PER_PAGE);

            posts = posts.OrderByDescending(x => x.CreatedTime).Skip((pn - 1) * POSTS_PER_PAGE).Take(POSTS_PER_PAGE);
            var postsList = posts.ToList();

            var vm = new HomeViewModel()
            {
                Category = category,
                Posts = posts.ToList(),
                PaginationInfo = new PaginationViewModel()
                {
                    CurrentPage = pn,
                    HasNewer = pn > 1,
                    HasOlder = pn < totalPages,
                    ItemsOnPage = postsList.Count(),
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    ItemsPerPage = POSTS_PER_PAGE,
                    ResultsStart = (pn - 1) * POSTS_PER_PAGE + 1,
                    ResultsEnd = (pn - 1) * POSTS_PER_PAGE + postsList.Count()
                },
                SearchCriteria = q

            };
            return View(vm);
        }

        [Route("p/{slug}")]
        public IActionResult ShowPost(string slug)
        {
            return View(_context.Posts
                .Include(x => x.Category)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Comments)
                    .ThenInclude(x => x.Children)
                        .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.Slug == slug));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public IActionResult Comment(int postId, string content, string slug, int? parentId)
        {
            
            if (string.IsNullOrWhiteSpace(content)) return BadRequest();

            _context.Add(new Comment()
            {
                AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreatedTime = DateTime.Now,
                ParentId = parentId,
                PostId = postId,
                Content = content,
                IsPublished = true
            });
            _context.SaveChanges();
            string url = Url.Action("ShowPost", new { slug, message = "received" }) +"#comments";
            return Redirect(url);
        }



    }
}
