using Microsoft.EntityFrameworkCore;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuBusiness.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService (AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<UserModel> GetById(string id)
        {
            var user =await _dbContext.Users.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(user == null)
            {
                string itemName = "Người dùng";
                throw new Exception(string.Format(Constants.Commons.ITEM_NOT_EXIST, itemName));
            }
            return new UserModel
            {
                HoTen = user.HoTen,
                Email = user.Email,
                SDT = user.PhoneNumber,
                UserName = user.UserName
            };
        }
    }
}
