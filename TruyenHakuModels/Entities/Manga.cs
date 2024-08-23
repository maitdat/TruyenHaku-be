using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Manga : BaseEntityCommon
    {
        public required string Name { get; set; }
        public string AnotherName { get; set; }
        public List<MangaCategory> MangaCategories { get; set; }
        public long TotalChapter { get; set; }
        public float LastChapter { get; set; }
        [Column("AuthorId")]
        public Author? Author { get; set; }
        public long TotalView { get; set; } 
        public string FolderPath { get; set; }
    }
}
