using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ContactManagement.Web.Providers
{
    public class FacebookAuthProvider : IFacebookAuthenticationProvider
    {
        public void ApplyRedirect(FacebookApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public Task ReturnEndpoint(FacebookReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }

}