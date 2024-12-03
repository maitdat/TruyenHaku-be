using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.ApplicationService.MangaService;
using TruyenHakuBusiness.DesignPattern.UnitOfWork;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.RequestModels.Application.Manga;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private IMangaService _mangaService;
        private IUnitofWork _unitofWork;
       
        public MangaController(IMangaService mangaService, IUnitofWork unitofWork)
        {
            _mangaService = mangaService;
            _unitofWork = unitofWork;
            
        }

       
        [HttpPost("{webCrawlId}")]
        public async Task<IActionResult> CrawlThenAddManga ([FromRoute]long webCrawlId, CreateMangaRequestModel createMangaRequestModel)
        {
            var res = await _mangaService.CrawlThenAddManga(webCrawlId, createMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest(res.Message);
        }

        [HttpPost("{webCrawlId}")]
        public async Task<IActionResult> CrawlThenAddListManga([FromRoute] long webCrawlId, List<CreateMangaRequestModel> listCreateMangaRequestModel)
        {
            var res = await _mangaService.CrawlThenAddListManga(webCrawlId, listCreateMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest(res.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddManga(CreateMangaRequestModel createMangaRequestModel)
        {
            var res = await _mangaService.AddManga(createMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest(res.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManga(long id)
        {
            var res = await _mangaService.GetManga(id);
            if (res.Id != 0)
                return Ok(res);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedManga([FromQuery] SearchFilterManga searchFilterManga)
        {
            var res = await _mangaService.GetPagedManga(searchFilterManga);
            if (res.Data.Count()>0)
                return Ok(res);
            return BadRequest();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveManga(long id)
        {
            var res = await _mangaService.RemoveManga(id);
            if (res.Succeed)
                return Ok(res);
            return BadRequest(res.Errors);
        }

    }
    
}
