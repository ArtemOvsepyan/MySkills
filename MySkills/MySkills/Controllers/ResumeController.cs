using Microsoft.AspNetCore.Mvc;
using MySkills.BL.Resume;

namespace MySkills.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResume resume;

        public ResumeController(IResume resume)
        {
            this.resume = resume;
        }

        [Route("/resume/{profileid}")]
        public async Task<IActionResult> Index(int profileid)
        {
            var model = await resume.Get(profileid);
            return View(model);
        }
    }
}
