using BlogNET.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Models
{
    public class NewPostViewModel
    {       
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; } = true;

        [PostPhoto(MaxSizeMB =1)]
        public IFormFile FeaturedImage { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public List<SelectListItem> Categories{ get; set; }

    }
}
