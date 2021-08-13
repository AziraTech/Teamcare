using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var loggedInUser = User;
            if (loggedInUser != null)
            {
                ViewData[ViewDataKeys.UserProfile] = new UserProfile
                {
                    FullName = loggedInUser.Claims.FirstOrDefault(i => i.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value,
                    Email = loggedInUser.Claims.FirstOrDefault(i => i.Type.Equals(ClaimTypes.PreferredUsername, StringComparison.OrdinalIgnoreCase))?.Value
                };
            }
            return base.OnActionExecutionAsync(context, next);
        }

        public void SetPageMetadata(string title, SiteSection activeSiteSection, IList<BreadcrumbItem> breadcrumbs)
        {
            var metadata = new PageMetadata
            {
                Title = title,
                ActiveSiteSection = activeSiteSection,
                Breadcrumbs = breadcrumbs
            };

            ViewData[ViewDataKeys.PageTitle] = title;

            ViewData[ViewDataKeys.PageMetadata] = metadata;
        }
    }
}
