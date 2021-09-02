using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.data.Entities;

namespace teamcare.web.app.Helpers
{
    public class CustomClaimsFactory : UserClaimsPrincipalFactory<User>
    {
        public CustomClaimsFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            return identity;
        }
    }
}
