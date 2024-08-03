using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Manga : BaseEntity
    {
        public required string Name { get; set; }
        public string AnotherName { get; set; }
        public Category Categories { get; set; }
        public string Chapter { get; set; }
        [Column("AuthorId")]
        public Author Author { get; set; }
        public long TotalView { get; set; }

    }
}
