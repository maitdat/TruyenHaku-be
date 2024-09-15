using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.CommonService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.ResponseModels;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private ICommonService _commonService;
        private const string _thumb = "Thumb";
        public MangaController(ICommonService commonService) 
        {
            _commonService = commonService;
        }
        [HttpGet]
        public async Task<BaseResponse> CrawlManga(string linkManga)
        {
            var web = new HtmlWeb();
            var document = web.Load(linkManga);

            var mangaName = document.DocumentNode.QuerySelector(".title-detail").InnerHtml;
            var otherMangaName = document.DocumentNode.QuerySelector(".other-name").InnerHtml;
            //var authorName = document.DocumentNode.QuerySelector(".author col-xs-8").InnerHtml;
            var thumbImgUrl = document.DocumentNode.QuerySelector(".image-thumb").Attributes["src"].Value;

            var listChapterHTMLElement = document.DocumentNode.QuerySelectorAll(".list-chapter > nav #desc li .chapter a ");

            var fileDirectory = "E:\\Data Manga\\ajin";

            await _commonService.DownloadImgFromURLAsync(thumbImgUrl, fileDirectory, $"{mangaName}{_thumb}");


            foreach ( var chapter in listChapterHTMLElement )
            {

            }


            return new BaseResponse()
            {
                Succeed = true,
            };
        }
    }

    public class MangaCrawl
    {
        public string AuthorName { get; set; }
        public string MangaName { get; set; }
        public string MangaDescription { get; set; }
        public string AnotherMangaName { get; set; }

    }
}
