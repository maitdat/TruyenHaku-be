using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebCommon.Constants;

namespace WebAPI.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string _roleName { get; set; }
        public AuthorizeAttribute(string roleName) 
        {
            _roleName = roleName;
        }
        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (_roleName.IsNullOrEmpty())
                {
                    if (context.HttpContext?.User?.Claims == null)
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
                else
                {
                    List<Claim> roleClaims = context.HttpContext?.User?.Claims.Where(X => X.Type == "role").ToList();
                    

                    var roles = new List<string>();
                    foreach (var role in roleClaims)
                    {
                        roles.Add(role.Value);
                    }
                    if (!roles.Contains(_roleName)) 
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }catch 
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
