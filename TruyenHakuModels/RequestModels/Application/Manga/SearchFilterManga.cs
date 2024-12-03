using TruyenHakuCommon;
using TruyenHakuCommon.Constants;

namespace TruyenHakuModels.RequestModels.Application.Manga
{
    public class SearchFilterManga : BasePaginationRequest
    {
        public List<long>? CategoryIdsSelected { get; set; }
        public List<long>? CategoryIdsUnselected { get; set; }
        public Enums.Status? Status { get; set; }
        public Enums.SortManga? SortBy { get; set; }
        public override int PageSize { get; set; } = Constants.Pagination.PAGE_SIZE_MANGA;
    }
}
