using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Slug { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}
