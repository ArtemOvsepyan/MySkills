using Microsoft.AspNetCore.Mvc;
using MySkills.Middleware;
using MySkills.BL.Auth;

namespace MySkills.Controllers
{
    [SiteAuthorize]
    public class LogoutController : Controller
    {
        private readonly IDbSession dbSession;

        public LogoutController(IDbSession dbSession)
        {
            this.dbSession = dbSession;
        }

        [Route("/logout")]
        public IActionResult LogOut()
        {
            dbSession.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
