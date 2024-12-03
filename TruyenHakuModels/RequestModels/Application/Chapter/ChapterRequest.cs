using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruyenHakuModels.RequestModels.Application.Chapter
{
    public class ChapterRequest
    {
        public long? Id { get; set; } // Null for create, filled for update
        public long MangaId { get; set; }
        public string Name { get; set; }
        public string PathDirectory { get; set; }
    }
}
