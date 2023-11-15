using Microsoft.AspNetCore.Mvc;
using MySkills.BL.Auth;
using MySkills.Middleware;
using MySkills.ViewModels;

namespace MySkills.Controllers
{
    [SiteNotAuthorize]
    public class LoginController : Controller
    {
        private readonly IAuth auth;

        public LoginController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
					await auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
					return Redirect("/");
				}
                catch (BL.AuthorizationException)
				{
                    ModelState.AddModelError("Email", "Email или пароль неверные");
                }
            }
            return View("Index", model);
        }
    }
}
