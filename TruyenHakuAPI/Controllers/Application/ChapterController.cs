using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.ApplicationService.ChapterService;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetChapter(long id)
        {
            var chapter = await _chapterService.GetChapterByIdAsync(id);
            if (chapter == null)
            {
                return NotFound("Chapter không tìm thấy");
            }
            return Ok(chapter);
        }

        [HttpGet("{mangaId}")]
        public async Task<IActionResult> GetChapters(long mangaId )
        {
            var response = await _chapterService.GetChaptersAsync(mangaId);
            return Ok(response);
        }
    }
}
