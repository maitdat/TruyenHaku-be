using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TruyenHakuBusiness.ApplicationService.MangaService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.RequestModels.Application.Manga;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private IMangaService _mangaService;
       
        public MangaController(IMangaService mangaService)
        {
            _mangaService = mangaService;
            
        }

       
        [HttpPost]
        public async Task<IActionResult> CrawlThenAddManga (CreateMangaRequestModel createMangaRequestModel)
        {
            var res = await _mangaService.CrawlThenAddManga(createMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddManga(CreateMangaRequestModel createMangaRequestModel)
        {
            var res = await _mangaService.AddManga(createMangaRequestModel);
            if (res.Succeed)
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManga(long id)
        {
            var res = await _mangaService.GetManga(id);
            if (res.Id != 0)
                return Ok(res);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{pathFolder}")]
        public async Task<IActionResult> GetChapter(string pathFolder)
        {
            if (Directory.Exists(pathFolder))
            {
                DirectoryInfo di = new DirectoryInfo(pathFolder);
                var files = di.GetFiles();

                var listImgsName = files.Select(x=>x.Name)
                    .OrderBy(x=>int.Parse(Regex.Match(x,@"\d+").Value))
                    .ToList();
                
                return Ok(listImgsName);
            }
            return BadRequest();    
        }

    }
    
}
