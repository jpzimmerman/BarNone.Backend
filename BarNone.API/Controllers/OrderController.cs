using BarNone.BusinessLogic.Services;
using BarNone.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <summary>
        /// Used for sending new guest order to backend. Endpoint will add order to the database, and process any actions necessary to update the user account where applicable.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary="Used for sending new guest order to backend. Endpoint will add order to the database, and process any actions necessary to update the user account where applicable.")]
        [HttpPut]
        [Route("AddOrder")]
        public async Task AddOrder([FromBody][SwaggerParameter(Description ="Guest order object",Required = true)]GuestOrder order)
        {
            await _menuDataService.AddOrder(order);
        }
    }
}
