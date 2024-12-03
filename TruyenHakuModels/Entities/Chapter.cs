using System.ComponentModel.DataAnnotations.Schema;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Chapter : BaseEntityCommon
    {
        [Column("MangaId")]
        public Manga Manga { get; set; }
        public required string Name { get; set; }
        public required string NameFolder { get; set; }
        public int Views { get; set; }
    }
}
