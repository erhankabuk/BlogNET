using BlogNET.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Models
{
    public class HomeViewModel
    {
        public Category Category { get; set; }
        public List<Post> Posts { get; set; }
        public PaginationViewModel PaginationInfo { get; set; }
        public string SearchCriteria { get; set; }
    }
}
