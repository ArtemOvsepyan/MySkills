using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySkills.BL.Auth;

namespace MySkills.Middleware
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class SiteNotAuthorizeAttribute: Attribute, IAsyncAuthorizationFilter
	{
		public SiteNotAuthorizeAttribute()
		{
		}

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>() ?? 
				throw new Exception("no user middleware");

            bool isLoggedIn = await currentUser.IsLoggedIn();
			if (isLoggedIn == true)
			{
				context.Result = new RedirectResult("/");
				return;
			}
		}
	}
}
