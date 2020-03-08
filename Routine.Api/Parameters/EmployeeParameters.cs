using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Parameters
{
    public class EmployeeParameters
    {
        private const int MaxPageSize = 10;
        public string SearchQuery { get; set; }
        public string Gender { get; set; }
        public string Q { get; set; }
        public int PageNumber { get; set; }
        private int pageSize = 5;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
    }
}