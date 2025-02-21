using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MenuController(MenuDataService menuDataService) : Controller
    {
        private readonly MenuDataService _menuDataService = menuDataService;

        [HttpGet]
        public string Index()
        {
            return "Found successfully";
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all menu items.")]
        [Route("GetMenuItems")]
        public async Task<ActionResult<List<MenuItem>>> GetMenuItems()
        {
            var menuItems = await _menuDataService.GetAllMenuItems();
            return Ok(menuItems);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all tags for menu items.")]
        [Route("GetTags")]
        public async Task<ActionResult<List<string>>> GetTags()
        {
            var tags = await _menuDataService.GetTags();
            return Ok(tags);
        }


    }
}
