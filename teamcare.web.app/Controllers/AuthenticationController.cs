using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using teamcare.business.Services;
using teamcare.data.Entities;
using teamcare.common.Helpers;
using teamcare.common.Enumerations;

namespace teamcare.web.app.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuditService _auditService;

        public AuthenticationController(IAuditService auditService)
		{
            _auditService = auditService;
        }
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

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = AuditAction.SignOut, Details = this.UserName+" has signed out.", UserReference = "", CreatedBy = this.UserId });
            });
            return new SignOutResult(new[] { "OpenIdConnect", "Cookies" }, new AuthenticationProperties
            {
                RedirectUri = "~/authentication/SignedOut"
            });
        }

        public IActionResult SignedOut()
        {
            return View();
        }

        public Guid? UserId
        {
            get
            {
                var userId = User.GetClaimValue(common.ReferenceData.ClaimTypes.UserId);
                return userId == null ? (Guid?)null : new Guid(userId);
            }
        }
        public string UserName
        {
            get
            {
                var userName = User.GetClaimValue(common.ReferenceData.ClaimTypes.Name);
                return userName == null ? "" : userName;
            }
        }
    }
}
