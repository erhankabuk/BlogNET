using BlogNET.Areas.Admin.Models;
using BlogNET.Areas.Admin.Services;
using BlogNET.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly UrlServices _urlService;

        public CategoriesController(ApplicationDbContext db, UrlServices urlService)
        {
            _db = db;
            _urlService = urlService;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
                return NotFound();


            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index", new { message = "delete" });
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult New(NewCategoryViewModel vm)
        {
            
            if (_db.Categories.Any(x => x.Name == vm.Name))
                return RedirectToAction(nameof(New), new { message = "categoryExisted" });

            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Name = vm.Name,
                    Slug = _urlService.URLFriendly(vm.Slug)
                };

                _db.Add(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            Category category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            var vm = new EditCategoryViewModel()
            {
                //Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel vm)
        {
            if (_db.Categories.Any(x => x.Name == vm.Name))
            {
                return RedirectToAction(nameof(Edit), new { message = "ExistedCategory" });
            }

            if (ModelState.IsValid)
            {
                Category category = _db.Categories.Find(vm.Id);
                if (category == null) return NotFound();
                //category.Id=vm.Id;
                category.Name = vm.Name;
                category.Slug = _urlService.URLFriendly(vm.Slug);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
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












