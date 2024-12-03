using TruyenHakuModels.RequestModels.Application.Manga;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Manga;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public interface IMangaService
    {
        Task<ResponseToClient> CrawlThenAddManga(long webCrawlId, CreateMangaRequestModel model);
        Task<ResponseToClient> CrawlThenAddListManga(long webCrawlId, List<CreateMangaRequestModel> models);
        Task<ResponseToClient> AddManga (CreateMangaRequestModel model);
        Task<GetInfoMangaResponse> GetManga(long id);
        Task<ResponseToClient> RemoveManga (long id);
        Task<BasePaginationResponse<GetInfoMangaResponse>> GetPagedManga(SearchFilterManga searchFilterManga);
    }
}
