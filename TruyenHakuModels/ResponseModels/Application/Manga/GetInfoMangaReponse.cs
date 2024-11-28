using TruyenHakuCommon;
using TruyenHakuModels.ResponseModels.Application.Author;

namespace TruyenHakuModels.ResponseModels.Application.Manga
{
    public class GetInfoMangaResponse : BaseEntity
    {
        public required string Name { get; set; }
        public string AnotherName { get; set; }
        public List<long> MangaCategories { get; set; }
        public long TotalChapter { get; set; }
        public float LastChapter { get; set; }
        public AuthorResponse? Author { get; set; }
        public long TotalView { get; set; }
        public string FolderPath { get; set; }
    }
}
