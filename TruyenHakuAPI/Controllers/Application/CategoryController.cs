using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruyenHakuBusiness.ApplicationService.CategoryService;
using TruyenHakuCommon.Constants;

namespace TruyenHakuAPI.Controllers.Application
{
    [Route(Constants.Controller.DEFAULT_ROUTE_CONTROLLER)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public ActionResult Get() {
            var res = _categoryService.GetCategories();
            return Ok(res);
        }
    }
}
