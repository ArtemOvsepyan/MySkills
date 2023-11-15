using Microsoft.AspNetCore.Mvc;
using MySkills.BL.Auth;

namespace MySkills.ViewComponents
{
	public class MainMenuViewComponent: ViewComponent
	{
		private readonly ICurrentUser currentUser;

		public MainMenuViewComponent(ICurrentUser currentUser)
		{ 
			this.currentUser = currentUser;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			bool isLoggedIn = await currentUser.IsLoggedIn();
			return View("Index", isLoggedIn);
		}
	}
}
