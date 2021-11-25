using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Data
{
    public class Post
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        public bool isPublished { get; set; } = true;

        [MaxLength(255)]
        public string PhotoPath { get; set; }

        [Required, MaxLength(200)]
        public string Slug { get; set; }

        [Required,ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
