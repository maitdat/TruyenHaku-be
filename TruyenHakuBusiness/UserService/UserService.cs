using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.Entities.Account;
using TruyenHakuModels.RequestModels.AuthRequestModel;
using TruyenHakuModels.ResponseModels.User;
using static TruyenHakuCommon.Constants.Constants;

namespace TruyenHakuBusiness.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserAccount> _userManager;
        public UserService (AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor, UserManager<UserAccount> userManager)
        {
            _dbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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

        public async Task<UserInfoResponse> GetCurrentUser()
        {
            var id = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid);
            var currentUser =await _userManager.FindByIdAsync(id);

            if(currentUser == null)
            {
                throw new Exception(string.Format(Constants.Commons.NOT_AUTHORIZED));
            }
            
            var userClaims =await _userManager.GetClaimsAsync(currentUser);

            return new UserInfoResponse
            {
                Id = currentUser.Id,
                FullName = currentUser.FullName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                UserName = currentUser.UserName,
                AvatarImg = userClaims?.FirstOrDefault(x => x.Type == ClaimTypesCustom.GOOGLE_AVATAR)?.Value,
                BirthDate = currentUser.BirthDate,
            };
        }
    }
}
