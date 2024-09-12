using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class MangaCategory : BaseEntityCommon
    {
        [Column("CategoryDefaultId")]
        public Category CategoryDefault { get; set; }

        [Column("MangaId")]
        public Manga Manga { get; set; }
    }
}
