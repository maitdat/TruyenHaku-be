using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.RequestModels.AuthRequestModel;
using TruyenHakuModels.ResponseModels.User;

namespace TruyenHakuBusiness.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService (AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserInfoResponse> GetById(string id)
        {
            var user =await _dbContext.Users.Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(user == null)
            {
                string itemName = "Người dùng";
                throw new Exception(string.Format(Constants.Commons.ITEM_NOT_EXIST, itemName));
            }
            return new UserInfoResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }

        //public async Task<RegisterRequest> GetUserByToken()
        //{
        //    var currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}
    }
}
