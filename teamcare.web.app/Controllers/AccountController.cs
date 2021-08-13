using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teamcare.web.app.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignOut()
        {
            return new SignOutResult(new[] { "OpenIdConnect", "Cookies" });
        }
    }
}
