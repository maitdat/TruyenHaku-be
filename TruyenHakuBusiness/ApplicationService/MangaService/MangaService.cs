using TruyenHakuModels;
using TruyenHakuModels.RequestModels.MangaRequestModel;

namespace TruyenHakuBusiness.ApplicationService.MangaService
{
    public class MangaService : IMangaService
    {
        private readonly AppDbContext _dbContext;
        public MangaService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<bool> AddNewManga(CreateMangaRequestModel model)
        {
            throw new NotImplementedException();
        }

        
        #region PRIVATE METHOd

        private bool CheckMangaExist(string name)
        {
            if(_dbContext.Manga)
            }

        #endregion
}
}
