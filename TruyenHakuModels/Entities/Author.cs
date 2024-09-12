using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        [Column("MangaId")]
        public required Manga Manga { get; set; }
    }
}
