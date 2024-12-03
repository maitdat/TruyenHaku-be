using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.ApplicationService.ChapterService;
using TruyenHakuCommon.Constants;
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

        [HttpGet]
        public async Task<IActionResult> GetChapters([FromQuery] BasePaginationRequest request)
        {
            var response = await _chapterService.GetChaptersWithPaginationAsync(request);
            return Ok(response);
        }
    }
}
