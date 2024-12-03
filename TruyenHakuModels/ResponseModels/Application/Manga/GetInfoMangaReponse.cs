using TruyenHakuCommon;
using TruyenHakuModels.ResponseModels.Application.Author;

namespace TruyenHakuModels.ResponseModels.Application.Manga
{
    public class GetInfoMangaResponse : BaseEntity
    {
        public required string Name { get; set; }
        public string? AnotherName { get; set; }
        public List<long> MangaCategories { get; set; }
        public long TotalChapter { get; set; }
        public string? LastChapter { get; set; }
        public AuthorResponse? Author { get; set; }
        public long TotalViews { get; set; }
        public long TotalLikes { get; set; }
        public string? Description { get; set; }
        public string MangaDirectory { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
