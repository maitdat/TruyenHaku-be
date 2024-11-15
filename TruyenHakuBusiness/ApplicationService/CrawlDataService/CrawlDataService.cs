using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using TruyenHakuBusiness.CommonService;
using TruyenHakuModels.ResponseModels;
using static TruyenHakuCommon.Constants.Constants;
using static TruyenHakuCommon.Utilities;

namespace TruyenHakuBusiness.ApplicationService.CrawlDataService
{
    public class CrawlDataService : ICrawlDataService
    {
        private ICommonService _commonService;

        private const string THUMB = "Thumb";

        public CrawlDataService(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public async Task<BaseResponse> CrawlManga(string linkManga)
        {
            var web = new HtmlWeb();
            var document = web.Load(linkManga);

            var mangaName = document.DocumentNode.QuerySelector($"{QuerySelectorBlogTruyenMoi.MANGA_NAME}").InnerHtml;
            //var otherMangaName = document.DocumentNode.QuerySelector(".other-name").InnerHtml;
            //var authorName = document.DocumentNode.QuerySelector(".author col-xs-8").InnerHtml;
            var thumbImgUrl = document.DocumentNode.QuerySelector($"{QuerySelectorBlogTruyenMoi.IMAGETHUMB}").Attributes["src"].Value;
            var listChapterTag = document.DocumentNode.QuerySelectorAll($"{QuerySelectorBlogTruyenMoi.LIST_CHAPTER}");
            var listChapter = listChapterTag.Select(x => new Chapter
            {
                NameChapter = x.InnerText,
                ChapterUrl = $"{QuerySelectorBlogTruyenMoi.HTTPS}{x.Attributes["href"].Value}"
            });


            var nameFolder = JoinEnDash(mangaName);


            var filePath = $"{PathFile.PATH_FILE_MANGA}\\{nameFolder?.ToLower()}";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (HttpClient client = new HttpClient())
            {
                await _commonService.DownloadImgFromURLAsync(client, thumbImgUrl, filePath, $"{mangaName}{THUMB}");
            }

            var tasks = new List<Task>();
            var fileChapterPath = "";

            var maxConcurrentChapters = 5; // Giới hạn số lượng chapter tải xuống cùng lúc
            var chapterSemaphore = new SemaphoreSlim(maxConcurrentChapters);

            foreach (var chapter in listChapter)
            {
                await chapterSemaphore.WaitAsync();  // Giới hạn số lượng chapter đồng thời
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var numberChapter = chapter.NameChapter.Split()[1];
                        var fileChapterPath = $"{filePath}\\{numberChapter}";
                        Directory.CreateDirectory(fileChapterPath);

                        var listImgUrls = GetListImgUrls(chapter.ChapterUrl);

                        // Thực hiện tải xuống danh sách ảnh trong chapter này
                        if (listImgUrls != null && listImgUrls.Any())
                        {
                            await _commonService.DownloadListImgFromURLAsync(listImgUrls, fileChapterPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing chapter {chapter.NameChapter}: {ex.Message}");
                    }
                    finally
                    {
                        chapterSemaphore.Release();  // Giải phóng một "slot" cho chapter mới
                    }
                }));
            }

            await Task.WhenAll(tasks);

            return new BaseResponse()
            {
                Succeed = true,
                Message = filePath
            };
        }

        private List<string> GetListImgUrls(string chapterUrl)
        {
            var web = new HtmlWeb();
            var document = web.Load(chapterUrl);

            List<string> listImgUrls = document.DocumentNode.QuerySelectorAll($"{QuerySelectorBlogTruyenMoi.IMGAGE}")
                .Select(x => x.GetAttributeValue($"{QuerySelectorBlogTruyenMoi.IMAGE_ATTRIBUTE}", "")).ToList();
            return listImgUrls;
        }
    }
    public class Chapter
    {
        public string ChapterUrl { get; set; }
        public string NameChapter { get; set; }

    }
}
