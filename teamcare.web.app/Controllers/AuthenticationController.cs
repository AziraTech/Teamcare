using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace teamcare.web.app.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult SignOut()
        {
            /*
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, OpenIdConnectDefaults.AuthenticationScheme,
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
                {
                    RedirectUri = "Account/SignedOut"
                });
            */
            return new SignOutResult(new[] { "OpenIdConnect", "Cookies" }, new AuthenticationProperties
            {
                RedirectUri = "~/authentication/SignedOut"
            });
        }

        public IActionResult SignedOut()
        {
            return View();
        }
    }
}
