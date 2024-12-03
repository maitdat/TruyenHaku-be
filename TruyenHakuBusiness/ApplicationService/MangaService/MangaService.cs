using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Net;
using TruyenHakuBusiness.CommonService;
using TruyenHakuBusiness.DesignPattern.Repository;
using TruyenHakuBusiness.DesignPattern.UnitOfWork;
using TruyenHakuCommon;
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
        private const string XOA_TRUYEN = "Xóa truyện";
        private const string THUMBNAIL = "Thumbnail";

        public MangaService(IUnitofWork unitOfWork, ICommonService commonService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
        }

        public async Task<ResponseToClient> CrawlThenAddManga(long webCrawlId, CreateMangaRequestModel model)
        {
            try
            {
                if (!IsMangaExisted(model.Name))
                {
                    var crawlResult = await CrawlManga(model, webCrawlId);
                    var categoriesDefault = _unitOfWork.Repository<Category>().GetAll();
                    var newManga = new Manga
                    {
                        Name = crawlResult.MangaName,
                        AnotherName = model.AnotherName,
                        MangaCategories = categoriesDefault.Where(x => model.CategoryIds.Contains(x.Id)).Select(y => new MangaCategory()
                        {
                            CategoryId = y.Id,
                        }).ToList(),
                        //Author = await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId),
                        NameFolder = crawlResult.FolderName,
                        Status = model.Status,
                        Chapters = crawlResult.Chapters.Select(x => new Chapter
                        {
                            Name = x.ChapterName,
                            NameFolder = x.FolderChapterName
                        }).ToList()
                    };

                    _unitOfWork.Repository<Manga>().Add(newManga);
                    await _unitOfWork.SaveChangesAsync();
                    return new ResponseToClient()
                    {
                        Succeed = true,
                    };
                }



                return new ResponseToClient()
                {
                    Errors = new[]
                    {
                    string.Format(Constants.Commons.ACTION_FAILED, THEM_TRUYEN)
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task<ResponseToClient> CrawlThenAddListManga(long webCrawlId, List<CreateMangaRequestModel> models)
        {
            foreach(var model in models)
            {
                var res = await CrawlThenAddManga(webCrawlId, model);
                if(!res.Succeed)
                    return new ResponseToClient()
                    {
                        Errors = new[]
                    {
                    string.Format(Constants.Commons.ACTION_FAILED, THEM_TRUYEN + model.MangaUrl)
                    }};
            }
            return new ResponseToClient()
            {
                Succeed = true,
            };
        }

        public async Task<ResponseToClient> AddManga(CreateMangaRequestModel model)
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
                        Author = model.AuthorId > 0 ? await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId) : null,
                        NameFolder = model.FolderPath,
                        Status = model.Status,
                        MangaCategories = categoriesDefault.Where(x => model.CategoryIds.Contains(x.Id)).Select(y => new MangaCategory()
                        {
                            CategoryId = y.Id,
                        }).ToList(),
                    };

                    _unitOfWork.Repository<Manga>().Add(newManga);
                    await _unitOfWork.SaveChangesAsync();
                    return new ResponseToClient()
                    {
                        Succeed = true,
                    };
                }
                return new ResponseToClient()
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

        public async Task<BasePaginationResponse<GetInfoMangaResponse>> GetPagedManga(SearchFilterManga searchFilterManga)
        {
            try
            {
                IEnumerable<long> mangaIdsMatched = Enumerable.Empty<long>();
                bool noFilter = true;
                if (searchFilterManga.CategoryIdsSelected !=null || searchFilterManga.CategoryIdsUnselected != null)
                {
                    noFilter = false;
                    mangaIdsMatched = _unitOfWork.Repository<MangaCategory>().Find(x =>
                        (searchFilterManga.CategoryIdsSelected == null|| searchFilterManga.CategoryIdsSelected.Contains(x.Id)) &&
                        (searchFilterManga.CategoryIdsUnselected == null  || !searchFilterManga.CategoryIdsUnselected.Contains(x.Id))
                        ).Select(x => x.Manga.Id);
                }

                var res1 = _unitOfWork.Repository<Manga>()
                    .Find(x =>
                    //(noFilter ||
                    //mangaIdsMatched.Contains(x.Id)
                    //)
                     x.Status == searchFilterManga.Status)
                    .Select(x => new GetInfoMangaResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        AnotherName = x.AnotherName,
                        MangaCategories = x.MangaCategories.Select(x => x.Id).ToList(),
                        TotalChapter = x.Chapters.Count(),
                        LastChapter = x.Chapters.OrderByDescending(x => x.Id).FirstOrDefault()?.Name,
                        //Author = x.
                        TotalViews = x.TotalViews,
                        TotalLikes = x.TotalLikes,
                        MangaDirectory = x.NameFolder,
                        Description = x.Description,
                        DateCreated = x.DateCreated.Value,
                        DateModified = x.DateModified.Value
                    });


                var res = _unitOfWork.Repository<Manga>()
                    .GetAll()
                    .Select(x => new GetInfoMangaResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        AnotherName = x.AnotherName,
                        MangaCategories = x.MangaCategories.Select(x => x.Id).ToList(),
                        TotalChapter = x.Chapters.Count(),
                        LastChapter = x.Chapters.OrderByDescending(x=>x.Id).FirstOrDefault().Name,
                        //Author = x.
                        TotalViews = x.TotalViews,
                        TotalLikes = x.TotalLikes,
                        MangaDirectory = x.NameFolder,
                        Description = x.Description,
                        DateCreated = x.DateCreated.Value,
                        DateModified = x.DateModified.Value
                    });


                if (searchFilterManga.SortBy != null)
                    SortManga(searchFilterManga.SortBy.Value, res);

                var totalItem = 0;
                Utilities.ApplyPaging(res, searchFilterManga.PageNo, searchFilterManga.PageSize, out totalItem);

                return new BasePaginationResponse<GetInfoMangaResponse>(searchFilterManga.PageNo, searchFilterManga.PageSize, res.ToList(), totalItem);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
           
        }

        

        public async Task<GetInfoMangaResponse> GetManga(long id)
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
                MangaDirectory = Constants.PathFile.DEFAULT_ROOT_DIRECTORY + manga.NameFolder,
                Author = manga.Author == null ? null : new AuthorResponse
                {
                    AuthorId = manga.Author.Id,
                    AuthorName = manga.Author.Name
                },

            };
            return result;
        }

        public async Task<ResponseToClient> RemoveManga(long id)
        {
            var manga = await _unitOfWork.Repository<Manga>().GetByIdAsync(id);
            if (manga != null)
            {
                _unitOfWork.Repository<Manga>().Remove(manga);
                return new ResponseToClient
                {
                    Succeed = true,
                };
            }
            return new ResponseToClient
            {
                Message = string.Format(Constants.Commons.ACTION_FAILED, XOA_TRUYEN)
            };
        }


        #region PRIVATE METHOD
        private IEnumerable<GetInfoMangaResponse> SortManga(Enums.SortManga sortBy, IEnumerable<GetInfoMangaResponse> mangas )
        {

            return sortBy switch
            {
                Enums.SortManga.CreatedDateDes => mangas.OrderByDescending(x => x.DateCreated),
                Enums.SortManga.CreatedDateAsc => mangas.OrderBy(x => x.DateCreated),
                Enums.SortManga.ModifiedDateDes => mangas.OrderByDescending(x => x.DateModified),
                Enums.SortManga.ModifiedDateAsc => mangas.OrderBy(x => x.DateModified),
                Enums.SortManga.TotalViewsAsc => mangas.OrderBy(x => x.TotalViews),
                _ => throw new ArgumentException("Invalid sorting option")
            };

        }

        private bool IsMangaExisted(string name)
        {
            var mangaFound = _unitOfWork.Repository<Manga>().Find(x => x.Name == name).FirstOrDefault();
            if (mangaFound != null)
                return true;
            return false;
            
        }

        #region CRAWL MANGA
        private async Task<MangaCrawl> CrawlManga(CreateMangaRequestModel request, long webCrawlId)
        {
            var webCssSelector = _unitOfWork.Repository<WebCssSelector>().Find(x => x.Id == webCrawlId).FirstOrDefault();
            if (webCssSelector == null)
                throw new Exception();

            var mangaCrawl = await GetDataFromHTML(request, webCssSelector);

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

        private async Task<MangaCrawl> GetDataFromHTML(CreateMangaRequestModel request, WebCssSelector webCssSelector)
        {
            try
            {
                // Lấy nội dung HTML sau khi JavaScript đã chạy
                var web = new HtmlWeb();

                var document =await web.LoadFromWebAsync(request.MangaUrl);

                MangaCrawl mangaCrawl = new MangaCrawl()
                {
                    MangaName = document.DocumentNode.QuerySelector(webCssSelector.MangaNameSelectors)?.InnerHtml ?? request.Name,
                    AnotherName = document.DocumentNode.QuerySelector(webCssSelector.AnotherNameSelectors)?.InnerHtml ?? request.AnotherName,
                    AuthorName = document.DocumentNode.QuerySelector(webCssSelector.AuthorSelectors)?.InnerHtml ?? "",
                    ThumbnailURL = document.DocumentNode.QuerySelector(webCssSelector.ImageThumbURLSelectors)?.Attributes["src"]?.Value,
                    Chapters = document.DocumentNode.QuerySelectorAll(webCssSelector.ListChapterSelectors).Select(x => new ChapterCrawl
                    {
                        ChapterName = x.InnerText,
                        ChapterUrl = $"{webCssSelector.Https}{x.Attributes["href"].Value}",
                        FolderChapterName = GetChapterNumber(x.InnerText),
                    }).ToList(),
                };

                var folderName = JoinEnDash(SanitizeFolderName(RemoveVietNameseChars(mangaCrawl.MangaName))).ToLower();
                mangaCrawl.FolderName = folderName;
                mangaCrawl.MangaDir = $"{PathFile.DEFAULT_ROOT_DIRECTORY}\\{folderName?.ToLower()}";

                return mangaCrawl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
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
                        var numberChapter = chapter.FolderChapterName;
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
                        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
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

        #endregion

        #region PRIVATE CLASS
        private class ChapterCrawl
        {
            public string? ChapterUrl { get; set; }
            public string? ChapterName { get; set; }
            public string? FolderChapterName { get; set; }
        }

        private class MangaCrawl
        {
            public string? MangaName { get; set; }
            public string? MangaDir { get; set; }
            public string? FolderName { get; set; }
            public string? AnotherName { get; set; }
            public string? AuthorName { get; set; }
            public string? ThumbnailURL { get; set; }
            public List<ChapterCrawl> Chapters { get; set; } = new List<ChapterCrawl>();
        }

        #endregion
    }
}
