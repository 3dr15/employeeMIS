using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Models
{
    public class Pagination
    {
        public int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set {
                _pageSize = (value > maxPageSize) ? maxPageSize : value; 
            }
        }

    }
}
