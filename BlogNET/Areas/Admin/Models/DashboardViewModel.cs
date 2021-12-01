using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoryCount { get; set; }
        public int PostCount { get; set; }
        public int UserCount { get; set; }
        //public int CommentCount { get; set; }
    }
}
