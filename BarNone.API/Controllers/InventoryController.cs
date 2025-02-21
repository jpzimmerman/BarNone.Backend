using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class InventoryController : Controller
    {
        private readonly InventoryDataService _inventoryDataService;

        public InventoryController(InventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
        }

        [HttpGet]
        public string Index()
        {
            return "Inventory page endpoint reached";
        }

        [HttpGet]
        [Route("GetInventoryItems")]
        public async Task<IEnumerable<Ingredient>> GetInventoryItems()
        {
            //return new List<Ingredient>()
            //{
            //    new Ingredient() {Name="Vodka", Quantity=6, IsAlcoholic=true},
            //    new Ingredient() {Name="Tequila", Quantity=4, IsAlcoholic=true},
            //    new Ingredient() {Name="White Rum", Quantity=3 ,IsAlcoholic=true},
            //    new Ingredient() {Name="Spiced Rum", Quantity=2, IsAlcoholic=true},
            //    new Ingredient() {Name="Grenadine", Quantity=4, IsAlcoholic=false},
            //};
            return await _inventoryDataService.GetInventoryItems();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("AddInventoryItem")]
        public async Task AddInventoryItem([FromBody]Ingredient data)
        {
            await _inventoryDataService.AddInventoryItem(data);
        }
    }
}
