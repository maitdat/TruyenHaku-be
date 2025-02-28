using Newtonsoft.Json;
using TruyenHakuCommon;
using TruyenHakuCommon.Constants;

namespace TruyenHakuModels.RequestModels.Application.Manga
{
    public class SearchFilterManga : BasePaginationRequest
    {
        public string? CategoryIdsSelected { get; set; }
        public string? CategoryIdsUnselected { get; set; }
        public Enums.Status? Status { get; set; }
        public Enums.SortManga? SortBy { get; set; }
        public override int PageSize { get; set; } = Constants.Pagination.PAGE_SIZE_MANGA;
    }
}
