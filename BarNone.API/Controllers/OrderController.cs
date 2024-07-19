using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class OrderController : Controller
    {
        private readonly MenuDataService _menuDataService;

        public OrderController(MenuDataService menuDataService)
        {
            _menuDataService = menuDataService;
        }

        [HttpPut]
        [Route("AddOrder")]
        public async Task AddOrder([FromBody]GuestOrder order)
        {
            await _menuDataService.AddOrder(order);
        }
    }
}
