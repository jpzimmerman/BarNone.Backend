using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MenuController : Controller
    {
        private readonly MenuDataService _menuDataService;

        public MenuController(MenuDataService menuDataService) 
        {
            _menuDataService = menuDataService;
        }

        [HttpGet]
        public string Index()
        {
            return "Found successfully";
        }

        [HttpGet]
        [Route("GetMenuItems")]
        public async Task<ActionResult<List<MenuItem>>> GetMenuItems()
        {
            var menuItems = await _menuDataService.GetAllMenuItems(); 
            return Ok(menuItems);
        }

        [HttpGet]
        [Route("GetTags")]
        public async Task<ActionResult<List<string>>> GetTags()
        {
            var tags = await _menuDataService.GetTags();
            return Ok(tags);
        }


    }
}
