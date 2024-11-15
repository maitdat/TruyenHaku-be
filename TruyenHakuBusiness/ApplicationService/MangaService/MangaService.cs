using TruyenHakuBusiness.ApplicationService.CrawlDataService;
using TruyenHakuBusiness.UnitOfWork;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels.MangaRequestModel;
using TruyenHakuModels.ResponseModels;

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
        public async Task<BaseResponse> AddManga(CreateMangaRequestModel model)
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
                            CategoryDefault = y,
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
        public async Task<>


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
