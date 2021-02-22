using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }

        public PagedResponse(T data, int pageNmuber, int pageSize)
        {
            PageNumber = pageNmuber;
            PageSize = pageSize;
            Data = data;
            Succeeded = true;
        }

    }
}
