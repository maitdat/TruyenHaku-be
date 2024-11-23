using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class MangaCategory : BaseEntityCommon
    {
        [Column("CategoryId")]
        public Category Category { get; set; }

        [Column("MangaId")]
        public Manga Manga { get; set; }
    }
}
