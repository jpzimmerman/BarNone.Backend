using BarNone.BusinessLogic.Interfaces;
using BarNone.BusinessLogic.Models;
using BarNone.BusinessLogic.Services;
using BarNone.DataLayer;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
        public async Task<ActionResult<List<IMenuItem>>> GetMenuItems()
        {
            var menuItems = await _menuDataService.GetAllMenuItems(); 
            return Ok(menuItems);
        }

        [HttpPut]
        [Route("AddMenuItem")]
        public async Task AddMenuItem(MenuItem menuItem)
        {
            await _menuDataService.AddMenuItemsToOrder(menuItem);
        }
    }
}
