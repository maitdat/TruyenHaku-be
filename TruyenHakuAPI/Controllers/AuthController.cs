using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TruyenHakuBusiness.AuthService;
using TruyenHakuBusiness.TokenService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities.Account;
using TruyenHakuModels.RequestModels.AuthRequestModel;

namespace TruyenHakuAPI.Controllers
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly UserManager<UserAccount> _userManager;
        private readonly IUserStore<UserAccount> _userStore;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService,
           SignInManager<UserAccount> signInManager,
           UserManager<UserAccount> userManager,
           IUserStore<UserAccount> userStore,
           ITokenService tokenService)
        {
            _authService = authService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest userinfo)
        {
            var res = await _authService.Login(userinfo);
            if (res.IsSucceed)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
        [HttpGet]
        public async Task<IActionResult> LoginThirdParty([FromQuery] ExternalLoginRequest request)
        {
            try
            {
                var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                var providerProcess = listprovider.Find((m) => m.Name.Equals(request.Provider, StringComparison.OrdinalIgnoreCase));

                if (providerProcess == null)
                {
                    return BadRequest(new { Message = "Dịch vụ không chính xác: " + request.Provider });
                }

                var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "AppUser", new { returnUrl = request.ReturnUrl });

                var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl);

                return new ChallengeResult(request.Provider, properties);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private readonly IReadOnlyDictionary<string, string> _claimsToSync =
        new Dictionary<string, string>()
        {
                 { "urn:google:picture", "https://localhost:5001/headshot.png" },
        };

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return BadRequest(new { Message = "Không thể lấy thông tin đăng nhập từ provider." });
                }

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
                if (result.Succeeded)
                {
                    // thay claim cũ cập nhật claim mới
                    var user = await _userManager.FindByLoginAsync(info.LoginProvider,
                        info.ProviderKey);
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    if (_claimsToSync.Count > 0)
                    {
                        bool refreshSignIn = false;

                        foreach (var addedClaim in _claimsToSync)
                        {
                            var userClaim = userClaims
                                .FirstOrDefault(c => c.Type == addedClaim.Key);

                            if (info.Principal.HasClaim(c => c.Type == addedClaim.Key))
                            {
                                var externalClaim = info.Principal.FindFirst(addedClaim.Key);

                                if (userClaim == null)
                                {
                                    await _userManager.AddClaimAsync(user,
                                        new Claim(addedClaim.Key, externalClaim.Value));
                                    refreshSignIn = true;
                                }
                                else if (userClaim.Value != externalClaim.Value)
                                {
                                    await _userManager
                                        .ReplaceClaimAsync(user, userClaim, externalClaim);
                                    refreshSignIn = true;
                                }
                            }
                            else if (userClaim == null)
                            {
                                // Fill with a default value
                                await _userManager.AddClaimAsync(user, new Claim(addedClaim.Key,
                                    addedClaim.Value));
                                refreshSignIn = true;
                            }
                        }

                        if (refreshSignIn)
                        {
                            await _signInManager.RefreshSignInAsync(user);
                        }
                    }

                    var token = await _tokenService.GenerateToken(user);

                    //var res = token;
                    //return Ok(res);
                    //Response.Headers.Append("X-Auth-Token", token);
                    return Redirect($"http://localhost:5173/auth-callback?token={token}");

                }
                else

                if (ModelState.IsValid)
                {
                    var user = new UserAccount()
                    {
                        Email = info.Principal.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                        UserName = info.Principal.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                        FullName = info.Principal.Claims.First(x => x.Type == ClaimTypes.Name).Value,
                        EmailConfirmed = true,
                    };


                    var result2 = await _userManager.CreateAsync(user);
                    if (result2.Succeeded)
                    {
                        result2 = await _userManager.AddLoginAsync(user, info);
                        if (result2.Succeeded)
                        {

                            // If they exist, add claims to the user for:
                            //    Picture

                            if (info.Principal.HasClaim(c => c.Type == "urn:google:picture"))
                            {
                                await _userManager.AddClaimAsync(user,
                                    info.Principal.FindFirst("urn:google:picture"));
                            }

                            var token = await _tokenService.GenerateToken(user);
                            return Ok(new
                            {
                                Token = token,
                                ReturnUrl = returnUrl ?? "/"
                            });
                        }
                    }
                }


                return BadRequest(new
                {
                    Token = "",
                    ReturnUrl = returnUrl ?? "/"
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
