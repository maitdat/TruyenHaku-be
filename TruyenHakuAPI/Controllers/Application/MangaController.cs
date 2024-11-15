using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.ApplicationService.CrawlDataService;
using TruyenHakuBusiness.ApplicationService.MangaService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.RequestModels.MangaRequestModel;
using TruyenHakuModels.RequestModels.RoleRequestModel;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private IMangaService _mangaService;
        private ICrawlDataService _crawlDataService;
        public MangaController(IMangaService mangaService, ICrawlDataService crawlDataService)
        {
            _mangaService = mangaService;
            _crawlDataService = crawlDataService;
        }
        [HttpPost]
        public async Task<IActionResult> CrawlAndAddNewManga(CreateMangaRequestModel createMangaRequestModel)
        {
            var res = await _mangaService.AddManga(createMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> CrawlData (string linkManga)
        {
            var res = await _crawlDataService.CrawlManga(linkManga);
            if(res.Succeed)
                return Ok();
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("aaaaa");
        }

    }
    
}
