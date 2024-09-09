using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.RequestModels.MangaRequestModel;
using TruyenHakuModels.ResponseModels;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public interface IMangaService
    {
        public Task<BaseResponse> AddManga(CreateMangaRequestModel model);
        
    }
}
