using TruyenHakuBusiness.ApplicationService.CrawlDataService;
using TruyenHakuBusiness.UnitOfWork;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels.Application.Manga;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Author;
using TruyenHakuModels.ResponseModels.Application.Manga;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public class MangaService : IMangaService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly ICrawlDataService _crawlDataService;

        private string THEM_TRUYEN = "Thêm truyện";


        public MangaService(IUnitofWork unitOfWork, ICrawlDataService crawlDataService)
        {
            _unitOfWork = unitOfWork;
            _crawlDataService = crawlDataService;
        }
        public async Task<BaseResponse> CrawlThenAddManga(CreateMangaRequestModel model)
        {
            if(!IsMangaExisted(model.Name))
            {
                var crawlResult = await _crawlDataService.CrawlManga(model.LinkManga);
                if (crawlResult.Succeed)
                {
                    var categoriesDefault = _unitOfWork.Repository<Category>().GetAll();
                    var newManga = new Manga
                    {
                        Name = model.Name,
                        AnotherName = model.AnotherName,
                        MangaCategories = categoriesDefault.Where(x => model.CategoryIds.Contains(x.Id)).Select(y => new MangaCategory()
                        {
                            Category = y,
                        }).ToList(),
                        Author = await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId),
                        FolderPath = crawlResult.Message,
                    };

                    _unitOfWork.Repository<Manga>().Add(newManga);
                    await _unitOfWork.SaveChangesAsync();
                    return new BaseResponse()
                    {
                        Succeed = true,
                    };
                }
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
                            Category = y,
                        }).ToList(),
                        Author = model.AuthorId > 0 ? await _unitOfWork.Repository<Author>().GetByIdAsync(model.AuthorId) : null,
                        FolderPath = model.FolderPath,
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
                ,x=>x.MangaCategories.Select(y=>y.Category));

            var result = new GetInfoMangaResponse
            {
                Name = manga.Name,
                AnotherName = manga.AnotherName,
                MangaCategories = manga.MangaCategories.Select(x => x.Category.CategoryEnum),
                TotalChapter = manga.TotalChapter,
                LastChapter = manga.LastChapter,
                TotalView = manga.TotalView,
                FolderPath = manga.FolderPath,
                Author = manga.Author == null ? null : new AuthorResponse
                {
                    AuthorId = manga.Author.Id,
                    AuthorName = manga.Author.Name
                }
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

        #region PRIVATE METHOd

        private bool IsMangaExisted(string name)
        {
            var mangaFound = _unitOfWork.Repository<Manga>().Find(x => x.Name == name).FirstOrDefault();
            if (mangaFound != null)
                return true;
            return false;
            
        }   
        #endregion
    }
}
