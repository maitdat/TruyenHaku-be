using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuCommon;

namespace TruyenHakuModels.Entities
{
    public class WebCssSelector : BaseEntity
    {
        public string? WebName {  get; set; }
        public string? MangaNameSelectors { get; set; }
        public string? AnotherNameSelectors { get; set; }
        public string? AuthorSelectors { get; set; }
        public string? ImageThumbURLSelectors { get; set; }
        public string? ListChapterSelectors { get; set; }
        public string? ImageSelectors { get; set; }
        public string? ImageAttribute { get; set; }
        public string? Https { get; set; }
    }

}
