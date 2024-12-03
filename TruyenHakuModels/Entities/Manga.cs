using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Manga : BaseEntityCommon
    {
        [MaxLength(200)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public string? AnotherName { get; set; }
        public ICollection<MangaCategory>? MangaCategories { get; set; } = new List<MangaCategory>();
        public ICollection<Chapter>? Chapters { get; set; } = new List<Chapter>();
        [Column("AuthorId")]
        public Author? Author { get; set; }
        [MaxLength(50)]
        public required string NameFolder { get; set; }
        public long TotalLikes {  get; set; }
        public long TotalViews {  get; set; }
        public Enums.Status Status { get; set; }
        [MaxLength(1200)]
        public string? Description { get; set; }
    }
}
