using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class MangaCategory : BaseEntityCommon
    {
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        [Column("MangaId")]
        public Manga Manga { get; set; }
    }
}
