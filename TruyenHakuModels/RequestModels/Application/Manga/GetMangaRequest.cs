namespace TruyenHakuModels.RequestModels.Manga
{
    public class GetMangaRequest
    {
        public List<int>? CategoryEnums { get; set; }
        public long? AuthorId { get; set; }
        public string FolderPath { get; set; }
    }
}
