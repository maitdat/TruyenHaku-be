using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
