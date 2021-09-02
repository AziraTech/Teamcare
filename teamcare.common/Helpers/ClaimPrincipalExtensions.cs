using System;
using System.Linq;
using System.Security.Claims;

namespace teamcare.common.Helpers
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal principal, string claimType)
        {
            var claim = principal?.Claims.FirstOrDefault(i =>
                i.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));

            return claim?.Value;
        }
    }
}
