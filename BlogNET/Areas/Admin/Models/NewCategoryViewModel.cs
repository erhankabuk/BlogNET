using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Models
{
    public class NewCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}
