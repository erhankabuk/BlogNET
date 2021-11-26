using BlogNET.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
       
    {
        private readonly ApplicationDbContext _db;

        public CategoriesViewComponent(ApplicationDbContext db )
        {
            _db = db;
        }    
        
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Categories.ToListAsync());
        }
    }
}
