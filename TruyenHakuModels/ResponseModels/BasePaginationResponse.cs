using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuModels.ResponseModels
{
    public class BasePaginationResponse<T>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
        public BasePaginationResponse(int pageNo, int pageSize, List<T> data, int totalItem)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalItems = totalItem;
            Data = data;
            TotalPages = (totalItem % pageSize) == 0 ? (totalItem / pageSize) : (totalItem / pageSize) + 1;
        }

    }
}
