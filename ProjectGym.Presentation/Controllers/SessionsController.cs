using Microsoft.AspNetCore.Mvc;
using ProjectGym.BusinessLogic.Servies;

namespace ProjectGym.Presentation.Controllers
{
    public class SessionsController(ISessionService sessions) : Controller
    {
        // Trainers View
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var items = await sessions.GetAllAsync(cancellationToken);


            return View(items);
        }

    }
}
