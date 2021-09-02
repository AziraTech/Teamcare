using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using teamcare.common.Enumerations;
using teamcare.common.Helpers;
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
                    FullName = loggedInUser.GetClaimValue(ClaimTypes.Name),
                    Email = loggedInUser.GetClaimValue(ClaimTypes.UserEmail)
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

        public string UserEmail
        {
            get
            {
                var email = User.GetClaimValue(common.ReferenceData.ClaimTypes.UserEmail);
                return email == null ? "" : email;
            }
        }
    }
}
