using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuCommon.Constants;

namespace TruyenHakuModels.RequestModels
{
    public class BasePaginationRequest
    {
        public string? Keyword { get; set; } = string.Empty;
        public int PageNo { get; set; } = Constants.Pagination.PAGE_NO_DEFAULT;
        public virtual int PageSize { get; set; } = Constants.Pagination.PAGE_SIZE_DEFAULT;

    }
}
