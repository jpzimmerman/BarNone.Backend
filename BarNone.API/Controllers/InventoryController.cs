using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class InventoryController : Controller
    {
        private readonly MenuDataService _menuDataService;

        [HttpGet]
        public string Index()
        {
            return "Inventory page endpoint reached";
        }

        [HttpPut]
        public async Task<IActionResult> AddInventoryItem(Ingredient data)
        {
        }
    }
}
