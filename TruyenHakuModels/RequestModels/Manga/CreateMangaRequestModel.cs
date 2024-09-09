namespace TruyenHakuModels.RequestModels.MangaRequestModel
{
    public class CreateMangaRequestModel
    {
        public required string Name { get; set; }
        public string? AnotherName { get; set; }
        public List<long> CategoryIds { get; set; }
        public long AuthorId { get; set; }
        public required string FolderPath { get; set; }
    }
}
