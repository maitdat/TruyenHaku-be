using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Manga : BaseEntityCommon
    {
        public required string Name { get; set; }
        public string? AnotherName { get; set; }
        public ICollection<MangaCategory>? MangaCategories { get; set; } = new List<MangaCategory>();
        public ICollection<Chapter>? Chapters { get; set; } = new List<Chapter>();
        public float LastChapter { get; set; }
        [Column("AuthorId")]
        public Author? Author { get; set; }
        public string? FolderPath { get; set; }
        public long TotalLikes {  get; set; }
        public Enums.Status Status { get; set; }
    }
}
