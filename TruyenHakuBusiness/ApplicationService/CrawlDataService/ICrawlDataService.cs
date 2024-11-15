using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.ResponseModels;

namespace TruyenHakuBusiness.ApplicationService.CrawlDataService
{
    public interface ICrawlDataService
    {
        Task<BaseResponse> CrawlManga(string linkManga);
    }
}
