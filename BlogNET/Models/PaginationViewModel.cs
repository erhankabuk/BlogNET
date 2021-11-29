using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Models
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int TotalPages{ get; set; }
        public int CurrentPage { get; set; }
        public int ItemsOnPage { get; set; }
        public bool HasNewer { get; set; }
        public bool HasOlder { get; set; }

    }
}
