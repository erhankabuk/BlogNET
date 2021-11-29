using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Models
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsOnPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int ResultsStart { get; set; }
        public int ResultsEnd{ get; set; }
        public bool HasNewer { get; set; }
        public bool HasOlder { get; set; }


        //https://gist.github.com/yigith/c6f999788b833dc3d22ac6332a053dd1
        //-1 refers to ... (gap between numbers)
        public int[] PageNumbers()
        {
            int current = CurrentPage;
            int last = TotalPages;
            int delta = 1;
            int left = current - delta;
            int right = current + delta + 1;
            var range = new List<int>();
            var rangeWithDots = new List<int>();
            int? l = null;

            for (var i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var i in range)
            {
                if (l != null)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots.Add(l.Value + 1);
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots.Add(-1);
                    }
                }
                rangeWithDots.Add(i);
                l = i;
            }

            return rangeWithDots.ToArray();
        }
    }
}
