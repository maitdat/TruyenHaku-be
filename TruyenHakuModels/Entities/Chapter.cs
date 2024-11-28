using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class Chapter : BaseEntityCommon
    {
        [Column("MangaId")]
        public Manga Manga { get; set; }
        public string Name { get; set; }
        public string PathDirectory { get; set; }
    }
}
