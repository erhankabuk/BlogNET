using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
            var user = new ApplicationUser()
            {
                Email = "admin@example.com",
                UserName = "admin@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "P@ssword1");
            await userManager.AddToRoleAsync(user, "admin");


        

        }
        public static async Task SeedCategoriesAndPostsAsync(ApplicationDbContext context)
        {
            var author = context.Users.FirstOrDefault(x => x.UserName == "admin@example.com");

            if (author == null || await context.Categories.AnyAsync()) return;
            
            var cat1 = new Category()
            {
                Name = "General",
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        Title="Welcome to My Blog",
                        Content="<p>Lorem ipsum dolor sit amet</p>",
                        PhotoPath="sample-photo-1.jpg",
                        Slug="welcome-to-my-blog",
                        Author=author,
                    },
                    new Post()
                    {
                        Title="A Sunny Day",
                        Content="<p>Lorem ipsum dolor sit amet</p>",
                        PhotoPath="sample-photo-2.jpg",
                        Slug="a-sunny-day",
                        Author=author,
                    }
                }
            };


            context.Categories.Add(cat1 );
            await context.SaveChangesAsync();

        }



    }
}
