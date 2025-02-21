using TruyenHakuCommon;
using TruyenHakuModels.ResponseModels.Application.Author;
using TruyenHakuModels.ResponseModels.Application.Category;
using TruyenHakuModels.ResponseModels.Application.Chapter;

namespace TruyenHakuModels.ResponseModels.Application.Manga
{
    public class GetInfoMangaResponse : BaseEntity
    {
        public required string Name { get; set; }
        public string? AnotherName { get; set; }
        public List<CategoryResponse>? MangaCategories { get; set; }
        public long TotalChapter { get; set; }
        public string? LastChapter { get; set; }
        public AuthorResponse? Author { get; set; }
        public long TotalViews { get; set; }
        public long TotalLikes { get; set; }
        public string? Description { get; set; }
        public string? NameFolder { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<ChapterResponse>? Chapters { get; set; }
    }
}
