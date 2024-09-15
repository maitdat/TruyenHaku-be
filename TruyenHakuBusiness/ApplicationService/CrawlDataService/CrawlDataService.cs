using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using TruyenHakuModels.ResponseModels;

namespace TruyenHakuBusiness.ApplicationService.CrawlDataService
{
    public class CrawlDataService
    {
        public async Task<BaseResponse> CrawlManga(string linkURL)
        {
            var web = new HtmlWeb();
            var document = web.Load(linkURL);
            var productHTMLElements = document.DocumentNode.QuerySelectorAll(".list-chapter > nav #desc li .chapter a ");

            return new BaseResponse()
            {
                Succeed = true,
            };
        }
    }
}
