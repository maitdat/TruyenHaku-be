using Microsoft.EntityFrameworkCore;
using WebCommon.Constants;
using WebModels;
using WebModels.RequestModels.AuthRequestModel;

namespace WebBusiness.UserService
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
                throw new Exception(Constants.Commons.USER_NOT_EXIST);
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
