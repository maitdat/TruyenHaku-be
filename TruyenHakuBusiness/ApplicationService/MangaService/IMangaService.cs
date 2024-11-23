using TruyenHakuModels.RequestModels.Application.Manga;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Manga;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public interface IMangaService
    {
        public Task<BaseResponse> CrawlThenAddManga(CreateMangaRequestModel model);
        public Task<BaseResponse> AddManga (CreateMangaRequestModel model);
        public Task<GetInfoMangaResponse> GetManga(long id);
        
    }
}
