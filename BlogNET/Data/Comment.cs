using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [MaxLength(450)]
        public string AuthorId { get; set; }
        public int PostId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public Post post { get; set; }
        public Comment Parent{ get; set; }
        public ICollection<Comment> Children { get; set; }
    }
}
