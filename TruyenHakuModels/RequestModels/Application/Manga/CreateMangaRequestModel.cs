using TruyenHakuCommon;

namespace TruyenHakuModels.RequestModels.Application.Manga
{
    public class CreateMangaRequestModel
    {
        public required string Name { get; set; }
        public string? AnotherName { get; set; }
        public List<long> CategoryIds { get; set; }
        public long AuthorId { get; set; }
        public string FolderPath { get; set; } = string.Empty;
        public required string MangaUrl { get; set; }
        public Enums.Status Status { get; set; }
    }
}
