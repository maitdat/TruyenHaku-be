using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuCommon;

namespace TruyenHakuModels.ResponseModels.Application.Chapter
{
    public class ChapterResponse  
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ChapterDir { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
