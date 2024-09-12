using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TruyenHakuBusiness.Repository;
using TruyenHakuBusiness.UnitOfWork;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels.MangaRequestModel;
using TruyenHakuModels.ResponseModels;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public class MangaService : IMangaService
    {
        private string themTruyen = "Thêm truyện";
        private readonly IUnitofWork _unitOfWork;
        public MangaService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse> AddManga(CreateMangaRequestModel model)
        {
            if(!CheckMangaExist(model.Name))
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
                    string.Format(Constants.Commons.ACTION_FAILED, themTruyen)
                }
            };
        }


        #region PRIVATE METHOd

        private bool CheckMangaExist(string name)
        {
            var mangaFound = _unitOfWork.Repository<Manga>().Find(x => x.Name == name).FirstOrDefault();
            if (mangaFound != null)
                return true;
            return false;
            
        }   
        #endregion
    }
}
