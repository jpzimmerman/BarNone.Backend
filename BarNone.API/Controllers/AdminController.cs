using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public string Index()
        {
            return "Viewing admin page";
        }
    }
}
