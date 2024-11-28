using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using TruyenHakuBusiness.CommonService;
using TruyenHakuBusiness.DesignPattern.UnitOfWork;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels.Application.Manga;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Author;
using TruyenHakuModels.ResponseModels.Application.Manga;
using static TruyenHakuCommon.Constants.Constants;
using static TruyenHakuCommon.Utilities;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public class MangaService : IMangaService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly ICommonService _commonService;

        private const string THEM_TRUYEN = "Thêm truyện";
        private const string THUMBNAIL = "Thumbnail";

        public MangaService(IUnitofWork unitOfWork, ICommonService commonService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
        }

        public async Task<BaseResponse> CrawlThenAddManga(CreateMangaRequestModel model)
        {
            if(!IsMangaExisted(model.Name))
            {
                var crawlResult = await CrawlManga(model.MangaUrl);
                var categoriesDefault = _unitOfWork.Repository<Category>().GetAll();
                var newManga = new Manga
                {
                    Name = crawlResult.MangaName,
                    AnotherName = model.AnotherName,
                    MangaCategories = categoriesDefault.Where(x => model.CategoryIds.Contains(x.Id)).Select(y => new MangaCategory()
                    {
                        Category = y,
                    }).ToList(),
                    //Author = await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId),
                    FolderPath = crawlResult.FolderName,
                    Status = model.Status
                };

                _unitOfWork.Repository<Manga>().Add(newManga);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse()
                {
                    Succeed = true,
                };
            }



            return new BaseResponse()
            {
                Errors = new[]
                {
                    string.Format(Constants.Commons.ACTION_FAILED, THEM_TRUYEN)
                }
            };
        }

        public async Task<BaseResponse> AddManga(CreateMangaRequestModel model)
        {
            try
            {
                if (!IsMangaExisted(model.Name))
                {
                    var categoriesDefault = _unitOfWork.Repository<Category>().GetAll();
                    var newManga = new Manga
                    {
                        Name = model.Name,
                        AnotherName = model.AnotherName,
                        MangaCategories = categoriesDefault.Where(x => model.CategoryIds.Contains(x.Id)).Select(y => new MangaCategory()
                        {
                            CategoryId = y.Id,
                        }).ToList(),
                        Author = model.AuthorId > 0 ? await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId) : null,
                        FolderPath = model.FolderPath,
                        Status = model.Status
                    };

                    _unitOfWork.Repository<Manga>().Add(newManga);
                    await _unitOfWork.SaveChangesAsync();
                    return new BaseResponse()
                    {
                        Succeed = true,
                    };
                }
                return new BaseResponse()
                {
                    Errors = new[]
                    {
                        string.Format(Constants.Commons.ACTION_FAILED, THEM_TRUYEN)
                    }
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
            
        }


        public async Task<GetInfoMangaResponse> GetManga (long id)
        {
            var manga = await _unitOfWork.Repository<Manga>().GetByIdAsync
                (id
                ,x=>x.Author
                ,x=>x.MangaCategories);

            var result = new GetInfoMangaResponse
            {
                Id = manga.Id,
                Name = manga.Name,
                AnotherName = manga.AnotherName,
                MangaCategories = manga.MangaCategories.Select(x => x.CategoryId).ToList(),
                FolderPath = manga.FolderPath,
                Author = manga.Author == null ? null : new AuthorResponse
                {
                    AuthorId = manga.Author.Id,
                    AuthorName = manga.Author.Name
                },

            };

            return result;
        }

        public async Task<GetInfoMangaResponse> GetImgsByChapterId(string chapter, string pathFolder)
        {
            if(!string.IsNullOrEmpty(chapter) && !Directory.Exists(pathFolder)) 
            {
                DirectoryInfo di= new DirectoryInfo(pathFolder);
                FileInfo[] files= di.GetFiles("*.jpg");

            }

            throw new NotImplementedException();
        }

        



        #region PRIVATE CLASS
        private class ChapterCrawl
        {
            public string ChapterUrl { get; set; }
            public string ChapterName { get; set; }
            public string ChapterDir { get; set; }
            public string FolderChapterName { get; set; }
        }

        private class MangaCrawl
        {
            public string MangaName { get; set; }
            public string MangaDir { get; set; }
            public string FolderName {  get; set; }
            public string AnotherName { get; set; }
            public string AuthorName { get; set; }
            public string ThumbnailURL { get; set; }
            public List<ChapterCrawl> Chapters { get; set; } = new List<ChapterCrawl>();
        }

        #endregion

        #region PRIVATE METHOD

        private async Task<MangaCrawl> CrawlManga(string mangaUrl)
        {

            var id = 1;
            var webCssSelector = _unitOfWork.Repository<WebCssSelector>().Find(x => x.Id == id).FirstOrDefault();
            if (webCssSelector == null)
                throw new Exception();

            var mangaCrawl = GetDataFromHTML(mangaUrl, webCssSelector);

            if (!Directory.Exists(mangaCrawl.MangaDir))
            {
                Directory.CreateDirectory(mangaCrawl.MangaDir);
            }

            using (HttpClient client = new HttpClient())
            {
                await _commonService.DownloadImgFromURLAsync(client, mangaCrawl.ThumbnailURL, mangaCrawl.MangaDir, $"{THUMBNAIL}");
            }

            await CrawlChapters(mangaCrawl.Chapters, mangaCrawl.MangaDir, webCssSelector);

            return mangaCrawl;
        }

        private bool IsMangaExisted(string name)
        {
            var mangaFound = _unitOfWork.Repository<Manga>().Find(x => x.Name == name).FirstOrDefault();
            if (mangaFound != null)
                return true;
            return false;
            
        }


        private MangaCrawl GetDataFromHTML(string mangaURL, WebCssSelector webCssSelector)
        {
            var web = new HtmlWeb();
            var document = web.Load(mangaURL);

           

            MangaCrawl mangaCrawl = new MangaCrawl()
            {
                MangaName = document.DocumentNode.QuerySelector(webCssSelector.MangaNameSelectors).InnerHtml,
                AnotherName = document.DocumentNode.QuerySelector(webCssSelector.AnotherNameSelectors).InnerHtml,
                AuthorName = document.DocumentNode.QuerySelector(webCssSelector.AuthorSelectors).InnerHtml,
                ThumbnailURL = document.DocumentNode.QuerySelector(webCssSelector.ImageThumbURLSelectors).Attributes["src"].Value,
                Chapters = document.DocumentNode.QuerySelectorAll(webCssSelector.ListChapterSelectors).Select(x => new ChapterCrawl
                {
                    ChapterName = x.InnerText,
                    ChapterUrl = $"{webCssSelector.Https}{x.Attributes["href"].Value}",
                    FolderChapterName = x.InnerText.Split()[1],
                }).ToList(),
            };
            var folderName = JoinEnDash(mangaCrawl.MangaName);
            mangaCrawl.FolderName = folderName;
            mangaCrawl.MangaDir = $"{PathFile.DEFAULT_ROOT_DIRECTORY}\\{folderName?.ToLower()}"; 
            
            return mangaCrawl;
        }

        private async Task CrawlChapters(List<ChapterCrawl> chapterCrawls, string mangaDir, WebCssSelector webCssSelector)
        {
            var tasks = new List<Task>();
            var maxConcurrentChapters = 5; // Giới hạn số lượng chapter tải xuống cùng lúc
            var chapterSemaphore = new SemaphoreSlim(maxConcurrentChapters);

            foreach (var chapter in chapterCrawls)
            {
                await chapterSemaphore.WaitAsync();  // Giới hạn số lượng chapter đồng thời
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var numberChapter = chapter.ChapterName.Split()[1];
                        var chapterDir = $"{mangaDir}\\{numberChapter}";

                        Directory.CreateDirectory(chapterDir);

                        var listImgUrls = GetImgUrlsFromHTML(chapter.ChapterUrl,webCssSelector.ImageSelectors, webCssSelector.ImageAttribute);

                        // Thực hiện tải xuống danh sách ảnh trong chapter này
                        if (listImgUrls != null && listImgUrls.Any())
                        {
                            await _commonService.DownloadListImgFromURLAsync(listImgUrls, chapterDir);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing chapter {chapter.ChapterName}: {ex.Message}");
                    }
                    finally
                    {
                        chapterSemaphore.Release();  // Giải phóng một "slot" cho chapter mới
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        private List<string> GetImgUrlsFromHTML(string chapterUrl, string imageSelector, string imageAttribute)
        {
            var web = new HtmlWeb();
            var document = web.Load(chapterUrl);

            List<string> listImgUrls = document.DocumentNode.QuerySelectorAll(imageSelector)
                .Select(x => x.GetAttributeValue(imageAttribute, "")).ToList();
            return listImgUrls;
        }

        #endregion
    }
}
