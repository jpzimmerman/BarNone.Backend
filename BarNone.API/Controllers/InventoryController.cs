using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class InventoryController(InventoryDataService inventoryDataService) : Controller
    {
        private readonly InventoryDataService _inventoryDataService = inventoryDataService;

        [HttpGet]
        public string Index()
        {
            return "Inventory page endpoint reached";
        }

        [HttpGet]
        [Route("GetInventoryItems")]
        public async Task<IEnumerable<Ingredient>> GetInventoryItems() => await _inventoryDataService.GetInventoryItems();

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("AddInventoryItem")]
        public async Task AddInventoryItem([FromBody]Ingredient data)
        {
            await _inventoryDataService.AddInventoryItem(data);
        }
    }
}
