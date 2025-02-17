using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarNone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AuthController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm]string returnUrl = "/", [FromForm]string provider = "Cookies")
        {
            switch (provider)
            {
                case "Google":
                    return Challenge(new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(Constants.LoginTtl),
                        RedirectUri = returnUrl,
                        AllowRefresh = false,
                    }, provider);
                case "Cookies":
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, "Admin")
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Redirect("/api/admin");
                default:
                    return Redirect("/api/admin");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            SignOut(new AuthenticationProperties(), CookieAuthenticationDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );
            await HttpContext.SignOutAsync();
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            return RedirectToAction("Index", "/");
        }
    }
}
